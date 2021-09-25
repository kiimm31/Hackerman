using ActionApi.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ActionApi.QuartzJob
{
    public class QuartzScheduler
    {
        public async Task Start(DateTime date)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            foreach (Type mytype in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                   .Where(mytype => mytype.GetInterfaces().Contains(typeof(IQuartzJob))))
            {
                //do stuff

                var qJob = (IQuartzJob)Activator.CreateInstance(mytype);

                IJobDetail job = JobBuilder.Create(mytype).Build();
                ITrigger trigger = TriggerBuilder.Create()
                 .WithIdentity(mytype.Name, "IDG")
                 .WithCronSchedule(qJob.CronExpression)
                   .StartAt(date)
                   .WithPriority(1)
                   .Build();
                await scheduler.ScheduleJob(job, trigger);
            }
        }
    }

    public static class QuartzDependencyInjection
    {
        public static void AddQuartz(this IServiceCollection services)
        {
            QuartzScheduler scheduler = new QuartzScheduler();
            _ = scheduler.Start(DateTime.Now);
        }
    }
}