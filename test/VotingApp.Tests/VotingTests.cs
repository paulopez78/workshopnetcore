using System;
using System.Collections.Generic;
using VotingApp.Domain;
using Xunit;

namespace VotingApp.Tests
{
    public class VotingAppTests
    {
        [Fact]
        public void Given_Topics_When_Start_Then_Voting_With_Votes_Created()
        {
            var voting = new VotingAggregate(Guid.NewGuid());
            voting.Start("DEV", "OPS");
            Assert.Equal(voting.State.Votes, new Dictionary<string, int>
            {
                {"DEV", 0},
                {"OPS", 0}
            });
        }

        [Fact]
        public void Given_Null_Topics_When_Start_Then_Exception()
        {
            var voting = new VotingAggregate(Guid.NewGuid());
            Action action = () => voting.Start(null);
            Assert.ThrowsAny<ArgumentNullException>(action);
        }

        [Fact]
        public void Given_One_Topic_When_Start_Then_Exception()
        {
            var voting = new VotingAggregate(Guid.NewGuid());
            Action action = () => voting.Start("DEV");
            Assert.ThrowsAny<InvalidOperationException>(action);
        }

        [Fact]
        public void Given_Topics_With_Same_Name_When_Start_Then_Exception()
        {
            var voting = new VotingAggregate(Guid.NewGuid());
            Action action = () => voting.Start("DEV", "DEV");
            Assert.ThrowsAny<ArgumentException>(action);
        }

        [Fact]
        public void Given_StartedVoting_When_Vote_Then_VoteCreated()
        {
            var voting = new VotingAggregate(Guid.NewGuid());
            voting.Start("DEV", "OPS");

            voting.Vote("DEV");
            Assert.Equal(voting.State.Votes, new Dictionary<string, int>() {
                {"DEV", 1},
                {"OPS", 0}
            });
        }

        [Fact]
        public void Given_StartedVoting_When_Vote_ForNotValid_Topic_Then_Same_Votes()
        {
            var voting = new VotingAggregate(Guid.NewGuid());
            voting.Start("DEV", "OPS");

            voting.Vote("DEVOPS");
            Assert.Equal(voting.State.Votes, new Dictionary<string, int>
            {
                {"DEV", 0},
                {"OPS", 0}
            });
        }

        [Fact]
        public void Given_StartedVoting_When_Finish_Then_Winner()
        {
            var voting = new VotingAggregate(Guid.NewGuid());
            voting.Start("DEV", "OPS");
            voting.Vote("DEV");
            voting.Vote("DEV");
            voting.Vote("OPS");

            voting.Finish();

            Assert.Equal("DEV", voting.State.Winner);
        }

        [Fact]
        public void Given_StartedVoting_When_Finish_With_Same_Votes_Then_No_Winner()
        {
            var voting = new VotingAggregate(Guid.NewGuid());
            voting.Start("DEV", "OPS");
            voting.Vote("DEV");
            voting.Vote("OPS");

            voting.Finish();
            Assert.Equal(string.Empty, voting.State.Winner);
        }

        [Fact]
        public void Given_SameTopics_When_StartVoting_Then_Exception()
        {
            var voting = new VotingAggregate(Guid.NewGuid());
            Action action = () => voting.Start("DEV", "DEV");

            Assert.ThrowsAny<ArgumentException>(action);
        }

        [Fact]
        public void Given_FinishedVoting_When_Vote_Then_Same_Votes()
        {
            var voting = new VotingAggregate(Guid.NewGuid());
            voting.Start("DEV", "OPS");
            voting.Vote("DEV");
            voting.Finish();

            voting.Vote("OPS");
            Assert.Equal(voting.State.Votes, new Dictionary<string, int>
            {
                {"DEV", 1},
                {"OPS", 0}
            });
        }

        [Fact]
        public void Given_NotStartedVoting_When_Finish_Then_No_Votes()
        {
            var voting = new VotingAggregate(Guid.NewGuid());
            voting.Finish();
            Assert.Equal(0, voting.State.Votes.Count);
        }

        [Fact]
        public void Given_NotStartedVoting_When_Vote_Then_No_Votes()
        {
            var voting = new VotingAggregate(Guid.NewGuid());
            voting.Vote("C#");
            Assert.Equal(0, voting.State.Votes.Count);
        }
    }
}
