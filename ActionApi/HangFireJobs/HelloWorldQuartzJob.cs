using Quartz;
using QuartzLibrary.Standard.QuartzJob;
using System;
using System.Threading.Tasks;

namespace QuartzJob
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
}
