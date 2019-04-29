using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;
using VotingApp.Api;
using VotingApp.Domain;
using System.Threading.Tasks;

namespace VotingApp.Tests
{
    public class VotingControllerTests
    {
        [Fact]
        public async Task Given_NotStartedVoting_When_Start_Then_SameState()
        {
            // Arrange and Act
            var votingService = new InMemoryVotingService();
            var voting = await votingService.Get();
            var controller = new VotingController(votingService, null, null);


            var result = await controller.Start(new string[] { "C#", "F#" });

            // Assert
            Assert.Equal(
                JsonConvert.SerializeObject(voting.GetState()),
                JsonConvert.SerializeObject(result));
        }

        [Fact]
        public async Task Given_StartedVoting_When_Vote_Then_SameState()
        {
            // Arrange 
            var votingService = new InMemoryVotingService();
            var voting = await votingService.Get();
            var controller = new VotingController(votingService, null, null);

            await controller.Start(new string[] { "C#", "F#" });

            // Act
            var result = await controller.Vote("C#");

            // Assert
            Assert.Equal(
                JsonConvert.SerializeObject(voting.GetState()),
                JsonConvert.SerializeObject(result));
        }

        [Fact]
        public async Task Given_StartedVoting_WithVotes_When_Finish_Then_SameState()
        {
            // Arrange and Act
            var votingService = new InMemoryVotingService();
            var voting = await votingService.Get();
            var controller = new VotingController(votingService, null, null);

            await controller.Start(new string[] { "C#", "F#" });
            await controller.Vote("C#");

            var result = await controller.Finish();

            // Assert
            Assert.Equal(
                JsonConvert.SerializeObject(voting.GetState()),
                JsonConvert.SerializeObject(result));
        }

    }
}
