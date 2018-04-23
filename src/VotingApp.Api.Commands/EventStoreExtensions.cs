using System;
using System.Collections.Generic;
using System.Reflection;
using Marten;
using Marten.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace VerificationProcessManager.EventStore
{
    public static class EventStoreExtensions
    {
        public static IServiceCollection AddEventStore(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSchema = "verification_manager";
            Retry(() => services.AddSingleton<IDocumentStore>(
                    DocumentStore.For(_ =>
                    {
                        _.Connection(configuration["dbconnection"] ?? "Host=localhost;Username=admin;Password=changeit;Database=voting");
                        _.Events.DatabaseSchemaName = databaseSchema;
                        _.DatabaseSchemaName = databaseSchema;
                        _.AutoCreateSchemaObjects = AutoCreate.All;
                        _.Events.InlineProjections.Add(new ManualVerificationsProjection());
                        _.Schema.For<ManualVerificationAggregate>().UseOptimisticConcurrency(true);
                    })));

            return services;
        }

        internal static void Retry(Action action, int retries = 5) =>
            Policy.Handle<Exception>()
                .WaitAndRetry(retries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .Execute(action);

    }
}