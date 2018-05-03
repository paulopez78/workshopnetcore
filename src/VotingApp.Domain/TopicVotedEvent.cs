namespace VotingApp.Domain
{
    public class TopicVotedEvent
    {
        public TopicVotedEvent(string topic) => this.Topic = topic;

        public string Topic { get; }
    }
}