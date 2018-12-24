using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Domain
{
  public class Voting
  {
    public Dictionary<string, int> Votes { get; private set; }
    public string Winner { get; private set; }

    public Voting(VotingDto votingDto)
    {
      Votes = votingDto.Votes;
      Winner = votingDto.Winner;
    }

    public void Start(params string[] options)
    {
      Votes = options.ToDictionary(x => x, _ => 0);
    }

    public void Vote(string topic, int step = 1)
    {
      Votes[topic] = Votes[topic] + step;
    }

    public void Finish()
    {
      var maxVotes = Votes.Max(x => x.Value);
      Winner = Votes.First(x => x.Value == maxVotes).Key;
    }

    public VotingDto VotingDto => new VotingDto
    {
      Votes = Votes,
      Winner = Winner
    };
  }
}