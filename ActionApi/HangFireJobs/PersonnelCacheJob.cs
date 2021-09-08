using Hangfire;
using System;

namespace ActionApi.HangFireJobs
{
    public class PersonnelCacheJob : IHangFireJob
    {
        public System.Action DoWork()
        {
            return () => Console.Write("Easy!");
        }

        public string CronExpression()
        {
            return Cron.Minutely();
        }
    }

    public interface IHangFireJob
    {
        public System.Action DoWork();

        public string CronExpression();
    }
}
