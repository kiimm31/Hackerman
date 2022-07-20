using System;
using Microsoft.Extensions.DependencyInjection;

namespace QuartzLibrary.Standard.QuartzJob
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