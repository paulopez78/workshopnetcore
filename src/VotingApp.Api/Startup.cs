using EasyWebSockets;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using VotingApp.Domain;

namespace VotingApp.Api
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton(new Voting());
      services.AddMvc();
      services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "Voting API", Version = "v1" }));
      services.AddEasyWebSockets();
      //   var redisConnectionString = Configuration.GetValue("REDIS", "localhost:6379");
      //   services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseExceptionHandler();
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Voting API v.1");
      });

      app.UseMvc();
      app.UseEasyWebSockets();
    }
  }
}
