using System.Collections.Generic;

namespace VotingApp.Domain
{
  public class VotingDto
  {
    public Dictionary<string, int> Votes { get; set; }
    public string Winner { get; set; }
  }
}