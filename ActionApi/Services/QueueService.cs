using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using TestApi.Commands;
using TestApi.Interfaces;
using TestApi.Models;
using TestApi.Models.Events;

namespace TestApi.Services
{
    public class QueueService : BackgroundService
    {
        private readonly IEventQueue _queue;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public QueueService(IEventQueue queue, IMediator mediator, ILogger<QueueService> logger)
        {
            _queue = queue;
            _mediator = mediator;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                IEventArgs workItem =
                    await _queue.DequeueAsync(stoppingToken);
                if (workItem != null)
                {
                    switch (workItem.GetType().Name)
                    {
                        case nameof(RandomEventArgs):
                            var result = await _mediator.Send(new GetRandomNumberCommand(), stoppingToken);

                            if (result.IsSuccess)
                            {
                                _logger.LogInformation($"{workItem.RequestDateTime.ToLocalTime()} : {result.Value}");
                            }
                            else
                            {
                                _logger.LogError(result.Error);
                            }
                            break;
                        default:
                            _logger.LogError($"Unhandled Event Type {workItem.GetType().Name}");
                            break;
                    }
                }
            }
        }
    }
}
