using ActionApi.HangFireJobs;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace ActionApi.Extensions
{
    public static class HangFireExtensions
    {

        public static void AddHangFire(this IServiceCollection services, string projectNamespacePrefix,
            IConfiguration configuration)
        {
            foreach (Type allImplementType in typeof(IHangFireJob).GetAllImplementTypes(projectNamespacePrefix))
                services.AddTransient(typeof(IHangFireJob), allImplementType);

            services.AddHangfire(globalConfiguration => globalConfiguration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));
            services.AddHangfireServer();
        }

        public static void UseHangFire(this IApplicationBuilder app)
        {
            IEnumerable<IHangFireJob> jobs = app.ApplicationServices.GetServices<IHangFireJob>();
            foreach (IHangFireJob hangFireJob in jobs)
            {
                RecurringJob.AddOrUpdate(() => hangFireJob.DoWork(), hangFireJob.CronExpression);
            }
        }
    }
}
