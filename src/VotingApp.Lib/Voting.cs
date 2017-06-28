using System;
using System.Collections.Generic;
using System.Linq;

namespace VotingApp.Lib
{
    public class Voting
    {
        public Dictionary<string, int> Votes { get; private set; }

        public string Winner { get; private set; }

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

        public void Vote(string topic)
        {
            AssertWinner();
            AssertValidTopic();
            
            Votes[topic] = ++Votes[topic];

            void AssertValidTopic()
            {
                topic = topic ?? throw new ArgumentNullException(nameof(topic));
                if (!Votes.ContainsKey(topic))
                {
                    throw new InvalidOperationException("Topic doesn't exist");
                }
            }

            void AssertWinner()
            {
                if (!string.IsNullOrEmpty(Winner))
                {
                    throw new InvalidOperationException($"The voting is over the winner is {Winner}");
                }
            }
        }

        public void Finish()
        {
            var maxVotes = Votes.Select(y => y.Value).Max();
            Winner = Votes.Single(x => x.Value == maxVotes).Key;
        }

        public object GetState() => new
        {
            Votes,
            Winner
        };
    }
}