using System;
using System.Linq;

namespace VotingApp.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var voting = new Voting();

            while (true)
            {
                try
                {
                    Console.WriteLine("Introduce a command:");
                    var commandParts = Console.ReadLine().Split(':');
                    var command = commandParts[0];
                    var parameters = commandParts.Length > 1 ? commandParts[1] : null;

                    switch (command)
                    {
                        case "start":
                            voting.Start(parameters?.Split(','));
                            Console.WriteLine($"TOPICS: {string.Join(",", voting.GetTopics())}");
                            break;
                        case "vote":
                            voting.Vote(parameters);
                            var votes = string.Join(",", voting.Votes.Select(x => $"{x.Key}:{x.Value}"));
                            Console.WriteLine($"VOTES: {votes}");
                            break;
                        case "finish":
                            voting.Finish();
                            Console.WriteLine($"WINNER: {voting.Winner}");
                            return;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
