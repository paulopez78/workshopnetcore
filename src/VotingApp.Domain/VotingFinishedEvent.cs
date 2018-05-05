
using System;

namespace VotingApp.Domain
{
    public class VotingFinishedEvent : VotingEvent
    {
        public VotingFinishedEvent(Guid id) : base(id)
        {
        }
    }
}