using Quartz;
using Quartz.Impl;
using QuartzLibrary.Standard.QuartzJob;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzJob
{
    public class QuartzScheduler
    {
        public async Task Start(DateTime date)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            var type = typeof(IQuartzJob);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));
            foreach (Type mytype in types)
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
}