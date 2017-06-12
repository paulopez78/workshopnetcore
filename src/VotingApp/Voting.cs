using System;
using System.Collections.Generic;
using System.Linq;

namespace VotingApp
{
    public class Voting
    {
        public IDictionary<string, int> Votes { get; private set; } = new Dictionary<string, int>();

        public string Winner { get; internal set; } = string.Empty;

        public string[] GetTopics() => Votes.Select(_ => _.Key).ToArray();

        public void Start(params string[] topics)
        {
            topics = topics ?? throw new ArgumentNullException(nameof(topics));
            Votes = topics.ToDictionary(topic => topic, _ => 0);
            Winner = string.Empty;
        }

        public void Vote(string topic)
        {
            if (!string.IsNullOrEmpty(Winner)) throw new InvalidOperationException($"The voting is over the winner is {Winner}");
            topic = topic ?? throw new ArgumentNullException(nameof(topic));
            if (!Votes.ContainsKey(topic)) throw new InvalidOperationException("Topic doesn't exist");
            Votes[topic] = ++Votes[topic];
        }

        public void Finish() =>
            Winner = Votes.Single(x => x.Value == Votes.Select(y => y.Value).Max()).Key;

        public object GetState() => new
        {
            Votes,
            Winner
        };
    }
}