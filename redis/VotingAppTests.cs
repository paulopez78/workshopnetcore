using System;
using System.Collections.Generic;
using VotingApp.Domain;
using Xunit;

namespace VotingApp.Tests
{
  public class VotingAppTests
  {

    [Fact]
    public void StarVoting_Test()
    {
      var voting = new Voting();
      var votingState = voting.Start("C#", "Java");
      Assert.Equal(new Dictionary<string, int> {
        { "C#", 0 },
        { "Java", 0 } },
        votingState.Votes);
    }

    [Fact]
    public void Vote_Testing()
    {
      var voting = new Voting();
      voting.Start("C#", "Java");
      var votingState = voting.Vote("C#");

      Assert.Equal(new Dictionary<string, int> {
        { "C#", 1 },
        { "Java", 0 } },
        votingState.Votes);
    }

    [Fact]
    public void Finish_Testing()
    {
      var voting = new Voting();
      voting.Start("C#", "Java");
      voting.Vote("C#");
      var votingState = voting.Finish();

      Assert.Equal("C#", votingState.Winner);
    }
  }
}
