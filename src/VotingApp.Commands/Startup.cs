﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VotingApp.Domain;

namespace VotingApp.Commands
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEventStore(Configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler();
            app.UseMvc();
        }
    }
}