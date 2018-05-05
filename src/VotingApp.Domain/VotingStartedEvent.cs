using System;

namespace VotingApp.Domain
{
    public class VotingStartedEvent : VotingEvent
    {
        public VotingStartedEvent(Guid id, string[] topics): base(id) => this.Topics = topics;

        public string[] Topics { get; }
    }
}