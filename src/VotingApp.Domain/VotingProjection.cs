using System;
using System.Collections.Generic;
using System.Linq;

namespace VotingApp.Domain
{
    public class VotingProjection
    {
        private Dictionary<string, int> _votes = new Dictionary<string, int>();

        public IReadOnlyDictionary<string, int> Votes { get => _votes; } 

        public string Winner { get; private set; }

        public VotingProjection(IEnumerable<object> events)
        {
            if (events == null) events = new List<object>();
            events.ToList().ForEach(Apply);
        }

        public bool ValidTopic(string topic) => OnGoingVoting() && Votes.ContainsKey(topic);

        public bool OnlyOneWinner()
        {
            if (Votes.Any())
            {
                var maxVotes = Votes.Select(y => y.Value).Max();
                return OnGoingVoting() && Votes.Count(x => x.Value == maxVotes) == 1;
            }

            return false;
        }

        private bool OnGoingVoting() => string.IsNullOrEmpty(Winner);

        public void Apply(object votingStartedEvent)
        {
            switch(votingStartedEvent)
            {
                case VotingStartedEvent started:
                    _votes = started.Topics.ToDictionary(topic => topic, _ => 0);
                    Winner = string.Empty;
                    break;

                case TopicVotedEvent voted:
                    _votes[voted.Topic] = ++_votes[voted.Topic];
                    break;

                case VotingFinishedEvent finished:
                    var maxVotes = Votes.Select(y => y.Value).Max();
                    Winner = Votes.Single(x => x.Value == maxVotes).Key;
                    break;
            }
        }
    }
}