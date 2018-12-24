using System.Collections.Generic;

namespace VotingApp.Domain
{
  public class VotingState
  {
    public Dictionary<string, int> Votes { get; set; }
    public string Winner { get; set; }
  }
}