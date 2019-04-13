using EasyWebSockets;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VotingApp.Domain;

namespace VotingApp.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new Voting());
            services.Configure<VotingOptions>(Configuration);
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
