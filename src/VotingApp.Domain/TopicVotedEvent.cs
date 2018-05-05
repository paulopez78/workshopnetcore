using System;

namespace VotingApp.Domain
{
    public class TopicVotedEvent : VotingEvent
    {
        public TopicVotedEvent(Guid id, string topic): base(id) => this.Topic = topic;

        public string Topic { get; }
    }
}