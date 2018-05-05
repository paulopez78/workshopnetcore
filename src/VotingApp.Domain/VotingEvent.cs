using System;

namespace VotingApp.Domain
{
    public abstract class VotingEvent
    {
        public Guid Id { get; }

        protected VotingEvent(Guid id) => Id = id;
    }
}