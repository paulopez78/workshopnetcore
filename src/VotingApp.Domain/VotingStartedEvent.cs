namespace VotingApp.Domain
{
    public class VotingStartedEvent
    {
        public VotingStartedEvent(string[] topics) => this.Topics = topics;

        public string[] Topics { get; }
    }
}