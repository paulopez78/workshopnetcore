using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyWebSockets;
using Microsoft.AspNetCore.Mvc;

namespace VotingApp.Api
{
    [Route("api/[controller]")]
    public class VotingController : Controller
    {
        private readonly Voting _voting;
        private readonly IWebSocketPublisher _wsPublisher;

        public VotingController(Voting voting, IWebSocketPublisher wsPublisher)
        {
            _voting = voting;
            _wsPublisher = wsPublisher;
        }

        [HttpGet]
        public object Get() =>
            _voting.GetState();

        [HttpPost]
        public object Start([FromBody]string[] topics) =>
            ExecuteCommand(() => _voting.Start(topics));

        [HttpPut]
        public object Vote([FromBody]string topic) =>
            ExecuteCommand(() => _voting.Vote(topic));

        [HttpDelete]
        public object Finish() =>
            ExecuteCommand(_voting.Finish);

        private object ExecuteCommand(Action action)
        {
            var votingState = _voting.GetState();
            
            action();
            _wsPublisher.SendMessageToAllAsync(votingState);
            
            return votingState;
        }
    }
}
