using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyWebSockets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using VotingApp.Domain;

namespace VotingApp.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VotingController : ControllerBase
  {
    private readonly ILogger<VotingController> _logger;
    private readonly IWebSocketPublisher _ws;
    private readonly IDatabase _redisDb;

    private const string VotingAppKey = "VotingKey";

    public VotingController(
      IWebSocketPublisher ws,
      IConnectionMultiplexer redis,
      ILogger<VotingController> logger)
    {
      _logger = logger;
      _ws = ws;
      _redisDb = redis.GetDatabase();
    }

    [HttpGet]
    public async Task<VotingState> Get() => await GetVotingState();

    [HttpPost]
    public async Task<VotingState> Start([FromBody] string[] topics) =>
      await Execute(v => v.Start(topics));

    [HttpPut]
    public async Task<VotingState> Vote([FromBody] string topic) =>
      await Execute(v => v.Vote(topic));

    [HttpDelete]
    public async Task<VotingState> Finish() =>
      await Execute(v => v.Finish());

    private async Task<VotingState> GetVotingState()
    {
      var votingState = await _redisDb.StringGetAsync(VotingAppKey);
      return (votingState.HasValue)
        ? JsonConvert.DeserializeObject<VotingState>(votingState)
        : new VotingState();
    }

    private async Task<VotingState> Execute(Func<Voting, VotingState> votingCommand)
    {
      var votingState = await GetVotingState();
      var voting = new Voting(votingState);

      var newVotingState = votingCommand(voting);

      await _redisDb.StringSetAsync(VotingAppKey, JsonConvert.SerializeObject(newVotingState));
      await _ws.SendMessageToAllAsync(newVotingState);
      return newVotingState;
    }
  }
}
