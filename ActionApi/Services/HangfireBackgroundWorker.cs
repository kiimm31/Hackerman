using ActionApi.HangFireJobs;
using Hangfire;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ActionApi.Services
{
    public class HangfireBackgroundWorker : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var type = typeof(IHangFireJob);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            if (types.Any())
            {
                foreach (var item in types)
                {
                    var job = (IHangFireJob)Activator.CreateInstance(item);

                    RecurringJob.AddOrUpdate(() => job.DoWork(), job.CronExpression);
                }
            }

            return Task.CompletedTask;
        }
    }
}
