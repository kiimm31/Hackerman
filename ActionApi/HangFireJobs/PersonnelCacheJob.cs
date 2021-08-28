using Hangfire;
using System;

namespace ActionApi.HangFireJobs
{
    public class PersonnelCacheJob : IHangFireJob
    {
        public string CronExpression()
        {
            return Cron.Minutely();
        }

        System.Action IHangFireJob.DoWork()
        {
            return () => Console.Write("Easy!");
        }
    }

    public interface IHangFireJob
    {
        public System.Action DoWork();

        public string CronExpression();
    }
}
