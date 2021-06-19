using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ActionApi.Services
{
    public class TwitchBackgroundService : IHostedService
    {
        private LiveMonitor _twitchMonitor;
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _twitchMonitor = new LiveMonitor();

            await _twitchMonitor.ConfigLiveMonitorAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _twitchMonitor.Dispose();
        }
    }
}
