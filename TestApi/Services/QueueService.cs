using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using TestApi.Interfaces;

namespace TestApi.Services
{
    public class QueueService : BackgroundService
    {
        private readonly IEventQueue _queue;

        public QueueService(IEventQueue queue)
        {
            _queue = queue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                IEventArgs workItem =
                    await _queue.DequeueAsync(stoppingToken);
                if (workItem != null)
                {
                   
                }
            }
        }
    }
}
