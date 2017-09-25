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
            var voting = new Voting();
            voting.Start("DEV", "OPS");
            Assert.Equal(voting.Votes, new Dictionary<string, int>
            {
                {"DEV", 0},
                {"OPS", 0}
            });
        }

        [Fact]
        public void Given_Null_Topics_When_Start_Then_Exception()
        {
            var voting = new Voting();
            Action action = () => voting.Start(null);
            Assert.ThrowsAny<ArgumentNullException>(action);
        }

        [Fact]
        public void Given_One_Topic_When_Start_Then_Exception()
        {
            var voting = new Voting();
            Action action = () => voting.Start("DEV");
            Assert.ThrowsAny<InvalidOperationException>(action);
        }

        [Fact]
        public void Given_Topics_With_Same_Name_When_Start_Then_Exception()
        {
            var voting = new Voting();
            Action action = () => voting.Start("DEV", "DEV");
            Assert.ThrowsAny<ArgumentException>(action);
        }

        [Fact]
        public void Given_StartedVoting_When_Vote_Then_VoteCreated()
        {
            var voting = new Voting();
            voting.Start("DEV", "OPS");

            voting.Vote("DEV");
            Assert.Equal(voting.Votes, new Dictionary<string, int>() {
                {"DEV", 1},
                {"OPS", 0}
            });
        }

        [Fact]
        public void Given_StartedVoting_When_Vote_ForNotValid_Topic_Then_Exception()
        {
            var voting = new Voting();
            voting.Start("DEV", "OPS");

            Action action = () => voting.Vote("DEVOPS");
            Assert.ThrowsAny<InvalidOperationException>(action);
        }

        [Fact]
        public void Given_StartedVoting_When_Finish_Then_Winner()
        {
            var voting = new Voting();
            voting.Start("DEV", "OPS");
            voting.Vote("DEV");
            voting.Vote("DEV");
            voting.Vote("OPS");

            voting.Finish();

            Assert.Equal(voting.Winner, "DEV");
        }

        [Fact]
        public void Given_StartedVoting_When_Finish_With_Same_Votes_Then_Exception()
        {
            var voting = new Voting();
            voting.Start("DEV", "OPS");
            voting.Vote("DEV");
            voting.Vote("OPS");

            Action action = () => voting.Finish();

            Assert.ThrowsAny<InvalidOperationException>(action);
        }

        [Fact]
        public void Given_SameTopics_When_StartVoting_Then_Exception()
        {
            var voting = new Voting();
            Action action = () => voting.Start("DEV", "DEV");

            Assert.ThrowsAny<ArgumentException>(action);
        }

        [Fact]
        public void Given_FinishedVoting_When_Vote_Then_Exception()
        {
            var voting = new Voting();
            voting.Start("DEV", "OPS");
            voting.Vote("DEV");
            voting.Finish();

            Action action = () => voting.Vote("OPS");

            Assert.ThrowsAny<InvalidOperationException>(action);
        }

        [Fact]
        public void Given_NotStartedVoting_When_Finish_Then_Exception()
        {
            var voting = new Voting();

            Action action = () => voting.Finish();

            Assert.ThrowsAny<InvalidOperationException>(action);
        }

        [Fact]
        public void Given_NotStartedVoting_When_Vote_Then_Exception()
        {
            var voting = new Voting();

            Action action = () => voting.Vote("C#");

            Assert.ThrowsAny<InvalidOperationException>(action);
        }
    }
}
