using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Marten;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Domain;

namespace VotingApp.Commands
{
    [Route("[controller]")]
    public class VotingController : Controller
    {
        private readonly IDocumentStore _eventStore;
        private readonly IBus _bus;

        public VotingController(IDocumentStore eventStore, IBus bus)
        {
            _eventStore = eventStore;
            _bus = bus;
        }

        [HttpPost]
        public async Task<object> Start([FromBody]string[] topics) =>
            await ExecuteCommand(aggregate => aggregate.Start(topics));

        [HttpPut]
        public async Task<object> Vote([FromBody]string topic) =>
            await ExecuteCommand(aggregate => aggregate.Vote(topic));

        [HttpDelete]
        public async Task<object> Finish() =>
            await ExecuteCommand(aggregate => aggregate.Finish());

        private async Task<object> ExecuteCommand(Action<VotingAggregate> action)
        {
            using(var session = _eventStore.OpenSession())
            {
                var currentVoting = await session.Query<CurrentVotingAggregate>().FirstOrDefaultAsync();
                var votingId = currentVoting?.Id ?? Guid.NewGuid();
                var events = (await session.Events.FetchStreamAsync(votingId)).Select(@event => @event.Data).ToArray();
                var aggregate = new VotingAggregate(votingId, events);

                action(aggregate);            

                if (aggregate.GetPendingEvents().Any())
                {
                    var eventStreamState = await session.Events.FetchStreamStateAsync(votingId);
                    var expectedVersion = (eventStreamState?.Version ?? 0) + aggregate.GetPendingEvents().Count();

                    // store events
                    session.Events.Append(votingId, expectedVersion, aggregate.GetPendingEvents().ToArray());
                    await session.SaveChangesAsync();

                    // publish events
                    await Task.WhenAll(aggregate.GetPendingEvents().Select(_bus.PublishAsync));
                }

                return aggregate.GetState();
            }
        }
    }
}
