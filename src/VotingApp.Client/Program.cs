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
                    var command = commandParts.First();
                    var parameters = commandParts.Last();

                    switch (command)
                    {
                        case "start":
                            voting.Start(parameters.Split(','));
                            Console.WriteLine($"TOPICS: {string.Join(",", voting.Topics)}");
                            break;
                        case "vote":
                            voting.Vote(parameters);
                            Console.WriteLine($"VOTES: {string.Join(",", voting.Votes)}");
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
