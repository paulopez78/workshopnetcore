using EasyWebSockets;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using VotingApp.Domain;

namespace VotingApp.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new Voting());
            services.AddMvc();
            services.AddEasyWebSockets();
            //   var redisConnectionString = Configuration.GetValue("REDIS", "localhost:6379");
            //   services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
        }

        public void Configure(IApplicationBuilder app) => app
            .UseDefaultFiles()
            .UseStaticFiles()
            .UseExceptionHandler()
            .UseMvc()
            .UseEasyWebSockets();
    }
}
