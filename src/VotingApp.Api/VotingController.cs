﻿using System;
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
        private readonly Voting _voting;
        private readonly IWebSocketPublisher _wsPublisher;
        private readonly int _votingStep;

        public VotingController(Voting voting, IWebSocketPublisher wsPublisher, IOptions<VotingOptions> options)
        {
            _voting = voting;
            _wsPublisher = wsPublisher;
            _votingStep = options?.Value?.VotingStep ?? 1;
        }

        [HttpGet]
        public object Get() =>
            _voting.GetState();

        [HttpPost]
        public async Task<object> Start([FromBody]string[] topics) =>
            await ExecuteCommand(() => _voting.Start(topics));

        [HttpPut]
        public async Task<object> Vote([FromBody]string topic) =>
            await ExecuteCommand(() => _voting.Vote(topic, _votingStep));

        [HttpDelete]
        public async Task<object> Finish() =>
            await ExecuteCommand(_voting.Finish);

        private async Task<object> ExecuteCommand(Action action)
        {
            action();
            var votingState = _voting.GetState();
            await PublishState();
            return votingState;

            async Task PublishState()
            {
                try
                {
                    await _wsPublisher.SendMessageToAllAsync(votingState);
                }
                catch (Exception)
                {
                    //Log
                }
            }
        }
    }
}
