using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyWebSockets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VotingApp.Domain;

namespace VotingApp.Api
{
    [Route("api/[controller]")]
    public class VotingController : Controller
    {
        private readonly IVotingService _votingService;
        private readonly IWebSocketPublisher _wsPublisher;
        private readonly int _votingStep;

        public VotingController(IVotingService votingService, IWebSocketPublisher wsPublisher, IOptions<VotingOptions> options)
        {
            _votingService = votingService;
            _wsPublisher = wsPublisher;
            _votingStep = options?.Value?.VotingStep ?? 1;
        }

        [HttpGet]
        public async Task<object> Get()
        {
            var voting = await _votingService.Get();
            return voting.GetState();
        }

        [HttpPost]
        public async Task<object> Start([FromBody]string[] topics) =>
            await ExecuteCommand(voting => voting.Start(topics));

        [HttpPut]
        public async Task<object> Vote([FromBody]string topic) =>
            await ExecuteCommand(voting => voting.Vote(topic, _votingStep));

        [HttpDelete]
        public async Task<object> Finish() =>
            await ExecuteCommand(voting => voting.Finish());

        private async Task<object> ExecuteCommand(Action<Voting> action)
        {
            var voting = await _votingService.Get();
            action(voting);
            await _votingService.Save(voting);
            await PublishState();
            return voting.GetState();

            async Task PublishState()
            {
                try
                {
                    await _wsPublisher.SendMessageToAllAsync(voting.GetState());
                }
                catch (Exception)
                {
                    //Log
                }
            }
        }
    }
}
