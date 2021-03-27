using DotNetCore.CAP;
using System.Threading.Tasks;
using TestApi.Interfaces;

namespace TestApi.Services
{
    public class CapPublishService
    {
        private readonly ICapPublisher _capPublisher;

        public CapPublishService(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        public async Task PublishAsync<T>(T eventArgs) where T : IEventArgs
        {
            await _capPublisher.PublishAsync(typeof(T).Name, eventArgs);
        }
    }
}