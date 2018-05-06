using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using EasyWebSockets;
using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using VotingApp.Domain;

namespace VotingApp.Queries
{
    [Route("api/[controller]")]
    public class VotingResultsService : Controller
    {
        private readonly IDocumentStore _eventStore;
        private readonly IBus _bus;
        private readonly IWebSocketPublisher _wsPublisher;
        private VotingProjection projection;

        public VotingResultsService(IDocumentStore eventStore, IBus bus, IWebSocketPublisher wsPublisher)
        {
            _eventStore = eventStore;
            _bus = bus;
            _wsPublisher = wsPublisher;
        }
        public object Get() => projection;

        public void Start()
        {
            using (var session = _eventStore.OpenSession())
            {
                var currentVoting = session.Query<CurrentVotingAggregate>().FirstOrDefault();
                var events = session.Events.FetchStream(currentVoting?.Id ?? Guid.Empty);
                projection = new VotingProjection(events);
            }

            _bus.SubscribeAsync<VotingEvent>("VotingEvents", async @event =>
            {
                projection.Apply(@event);
                await _wsPublisher.SendMessageToAllAsync(projection);
            });
        }
    }

    public static class VotingResultsServiceExtensions
    {
        public static IServiceCollection AddVotingResultsService(this IServiceCollection services, IConfiguration configuration) => services
            .AddEasyWebSockets()
            .AddSingleton<IBus>(RabbitHutch.CreateBus(configuration["messagebroker"]?.Trim()))
            .AddSingleton<VotingResultsService>();

        public static IApplicationBuilder UseVotingResultsService(this IApplicationBuilder app)
        {
            app.UseEasyWebSockets();
            var quizResultsEventHandler = app.ApplicationServices.GetService<VotingResultsService>();
            Retry(quizResultsEventHandler.Start);
            return app;
        }
        private static void Retry(Action action, int retries = 5) =>
            Policy.Handle<Exception>()
                .WaitAndRetry(retries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .Execute(action);
    }
}
