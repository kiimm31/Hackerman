using DotNetCore.CAP;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestApi.Commands;
using TestApi.Models;

namespace TestApi.Services
{
    public class CapMonitorService : BackgroundService, ICapSubscribe
    {
        private readonly ILogger<CapMonitorService> _logger;
        private readonly IMediator _mediator;

        public CapMonitorService(ILogger<CapMonitorService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"{nameof(CapMonitorService)} is alive..");

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        [CapSubscribe(nameof(RandomEventArgs))]
        public void RandomTask(RandomEventArgs randomEvent)
        {
            _mediator.Send(new QueueEventCommand()
            {
                Event = randomEvent
            });
        }
    }
}
