using Quartz;
using System;
using System.Threading.Tasks;

namespace ActionApi.QuartzJob
{
    public class HelloWorldQuartzJob : IQuartzJob
    {
        public string CronExpression { get => "* * * * * ? *"; }

        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"{DateTime.Now} -- HelloWorld");
            return Task.CompletedTask;
        }
    }

    public interface IQuartzJob : IJob
    {
        public string CronExpression { get; }
    }


}
