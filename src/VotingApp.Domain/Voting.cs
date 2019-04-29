using System;
using System.Collections.Generic;
using System.Linq;

namespace VotingApp.Domain
{
    public class Voting
    {
        public Dictionary<string, int> Votes { get; private set; }

        public string Winner { get; private set; }

        public Voting(Dictionary<string, int> votes, string winner)
        {
            Votes = votes;
            Winner = winner;
        }
        public Voting()
        {

        }

        public void Start(params string[] topics)
        {
            AssertValidTopics();

            Votes = topics.ToDictionary(topic => topic, _ => 0);
            Winner = string.Empty;

            void AssertValidTopics()
            {
                topics = topics ?? throw new ArgumentNullException(nameof(topics));
                if (topics.Length <= 1)
                {
                    throw new InvalidOperationException("Provide at least 2 topics for start voting");
                }
            }
        }

        public void Vote(string topic, int votingStep = 1)
        {
            AssertOnGoingVoting();
            AssertValidTopic();

            Votes[topic] = Votes[topic] + votingStep;

            void AssertValidTopic()
            {
                topic = topic ?? throw new ArgumentNullException(nameof(topic));
                if (!Votes.ContainsKey(topic))
                {
                    throw new InvalidOperationException("Topic doesn't exist");
                }
            }
        }

        public void Finish()
        {
            AssertOnGoingVoting();
            var maxVotes = Votes.Select(y => y.Value).Max();

            AssertOnlyOneWinner();
            Winner = Votes.Single(x => x.Value == maxVotes).Key;

            void AssertOnlyOneWinner()
            {
                if (Votes.Count(x => x.Value == maxVotes) != 1)
                {
                    throw new InvalidOperationException("Can't finish voting, only one winner is allowed");
                }
            }
        }

        public object GetState() => new
        {
            Votes,
            Winner
        };

        private void AssertOnGoingVoting()
        {
            if (Votes == null)
            {
                throw new InvalidOperationException("Voting not started yet");
            }

            if (!string.IsNullOrEmpty(Winner))
            {
                throw new InvalidOperationException($"The voting is over the winner is {Winner}");
            }
        }
    }
}