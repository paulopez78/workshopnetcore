using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace VotingApp.Api
{
    public class Program
    {
        public static void Main(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(
                    (hostContext, config) => config.AddJsonFile("secrets/appsettings.secrets.json", optional: true))
                .UseStartup<Startup>()
                .Build()
                .Run();
    }
}
