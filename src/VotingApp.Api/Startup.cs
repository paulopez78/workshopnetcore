using System;
using System.Threading.Tasks;
using EasyWebSockets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using VotingApp.Domain;

namespace VotingApp.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddHealthChecks()
            //     .AddCheck("liveness",
            //     () => DateTime.Now.Second > 30 ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy(),
            //     new[] { "live" })

            //     .AddCheck("readiness",
            //     () => DateTime.Now.Second > 30 ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy(),
            //     new[] { "ready" });

            services.AddSingleton<IVotingService>(_ =>
                Configuration["mongodb"] == null
                    ? new InMemoryVotingService()
                    : (IVotingService)new MongoDbVotingService(Configuration["mongodb"])
            );

            services.Configure<VotingOptions>(Configuration);
            services.AddMvc();
            services.AddEasyWebSockets();
        }

        public void Configure(IApplicationBuilder app) => app
            .UseDefaultFiles()
            .UseStaticFiles()
            .UseExceptionHandler()
            // .UseHealthChecks("/hc/ready", new HealthCheckOptions
            // {
            //     Predicate = check => check.Tags.Contains("ready"),
            // })
            // .UseHealthChecks("/hc/live", new HealthCheckOptions
            // {
            //     Predicate = check => check.Tags.Contains("live"),
            // })
            .UseMvc()
            .UseEasyWebSockets();
    }
}
