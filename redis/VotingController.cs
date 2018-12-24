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
    public async Task<VotingDto> Get() => await GetVotingDto();

    [HttpPost]
    public async Task<VotingDto> Start([FromBody] string[] topics) =>
      await Execute(v => v.Start(topics));

    [HttpPut]
    public async Task<VotingDto> Vote([FromBody] string topic) =>
      await Execute(v => v.Vote(topic));

    [HttpDelete]
    public async Task<VotingDto> Finish() =>
      await Execute(v => v.Finish());

    private async Task<VotingDto> GetVotingDto()
    {
      var votingState = await _redisDb.StringGetAsync(VotingAppKey);
      return JsonConvert.DeserializeObject<VotingDto>(votingState);
    }

    private async Task<VotingDto> Execute(Action<Voting> votingCommand)
    {
      var votingDto = await GetVotingDto();
      var voting = new Voting(votingDto);
      votingCommand(voting);
      await _redisDb.StringSetAsync(VotingAppKey, JsonConvert.SerializeObject(voting.VotingDto));
      await _ws.SendMessageToAllAsync(votingDto);
      return votingDto;
    }
  }
}
