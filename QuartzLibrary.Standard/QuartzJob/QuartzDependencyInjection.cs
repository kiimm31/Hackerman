using Microsoft.Extensions.DependencyInjection;
using System;

namespace QuartzJob
{
    public static class QuartzDependencyInjection
    {
        public static void AddQuartz(this IServiceCollection services)
        {
            QuartzScheduler scheduler = new QuartzScheduler();
            _ = scheduler.Start(DateTime.Now);
        }
    }
}