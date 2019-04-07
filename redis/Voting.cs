using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Domain
{
    public class Voting
    {
        private readonly VotingState _state;
        public Voting(VotingState state)
        {
            _state = state;
        }

        public Voting()
        {
            _state = new VotingState();
        }

        public VotingState Start(params string[] options)
        {
            _state.Votes = options.ToDictionary(x => x, _ => 0);
            return _state;
        }

        public VotingState Vote(string topic, int step = 1)
        {
            _state.Votes[topic] = _state.Votes[topic] + step;
            return _state;
        }

        public VotingState Finish()
        {
            _state.Winner = _state.Votes.Aggregate((a, b) => a.Value > b.Value).Key;
            return _state;
        }
    }
}