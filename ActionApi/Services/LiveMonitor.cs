using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.Services;
using TwitchLib.Api.Services.Events;
using TwitchLib.Api.Services.Events.LiveStreamMonitor;

namespace ActionApi.Services
{
    public class LiveMonitor : IDisposable
    {
        private LiveStreamMonitorService Monitor;
        private TwitchAPI API;
        public async Task ConfigLiveMonitorAsync()
        {
            API = new TwitchAPI();
            API.Settings.ClientId = "jxjz93r6z6x0n09a7khp6wq4dvle8k";
            API.Settings.Secret = "mlabuaroc67pgokd5kc8hqulokkmsd";
            API.Settings.AccessToken = API.Helix.Extensions.GetAccessToken();

            Monitor = new LiveStreamMonitorService(API, 60);
            List<string> lst = new List<string> { "67794893" };
            //Monitor.SetChannelsById(lst);
            Monitor.SetChannelsById(lst);

            Monitor.OnStreamOnline += Monitor_OnStreamOnline;
            Monitor.OnStreamOffline += Monitor_OnStreamOffline;
            Monitor.OnStreamUpdate += Monitor_OnStreamUpdate;
            Monitor.OnServiceStarted += Monitor_OnServiceStarted;
            Monitor.OnChannelsSet += Monitor_OnChannelsSet;
            Monitor.Start(); //Keep at the end!
            await Task.Delay(-1);
        }
        private void Monitor_OnStreamOnline(object sender, OnStreamOnlineArgs e)
        {
        }
        private void Monitor_OnStreamUpdate(object sender, OnStreamUpdateArgs e)
        {
        }
        private void Monitor_OnStreamOffline(object sender, OnStreamOfflineArgs e)
        {
        }
        private void Monitor_OnChannelsSet(object sender, OnChannelsSetArgs e)
        {
        }
        private void Monitor_OnServiceStarted(object sender, OnServiceStartedArgs e)
        {
        }

        public void Dispose()
        {
            Monitor.Stop();
        }
    }
}