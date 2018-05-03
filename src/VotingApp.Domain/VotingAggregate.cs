using System;
using System.Collections.Generic;
using System.Linq;

namespace VotingApp.Domain
{
    public class VotingAggregate
    {
        private readonly List<object> _pendingEvents = new List<object>();

        public VotingProjection State { get; }
        public object GetState() => new
        {
            State.Votes,
            State.Winner
        };

        public IReadOnlyList<object> GetPendingEvents() => _pendingEvents; 

        public VotingAggregate(IEnumerable<object> events = null) => State = new VotingProjection(events);

        public void Start(params string[] topics)
        {
            AssertValidTopics();
            RaiseEvent(new VotingStartedEvent(topics));

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
            if (State.ValidTopic(topic))
            {
                RaiseEvent(new TopicVotedEvent(topic));
            }
        }

        public void Finish()
        {
            if (State.OnlyOneWinner())
            {
                RaiseEvent(new VotingFinishedEvent());
            }
        }

        private void RaiseEvent(object votingStartedEvent)
        {
            _pendingEvents.Add(votingStartedEvent);
            State.Apply(votingStartedEvent);
        }
    }
}