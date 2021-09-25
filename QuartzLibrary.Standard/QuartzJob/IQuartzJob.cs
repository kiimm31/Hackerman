using Quartz;

namespace QuartzLibrary.Standard.QuartzJob
{
    public interface IQuartzJob : IJob
    {
        public string CronExpression { get; }
    }
}
