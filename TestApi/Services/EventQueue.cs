using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using TestApi.Interfaces;

namespace TestApi.Services
{
    public class EventQueue : IEventQueue
    {
        private ConcurrentQueue<IEventArgs> _workItems =
            new ConcurrentQueue<IEventArgs>();

        private SemaphoreSlim _signal = new SemaphoreSlim(0);

        public EventQueue()
        {

        }

        public void QueueRequestEvent(IEventArgs requestEvent)
        {
            _workItems.Enqueue(requestEvent);
            _signal.Release();
        }

        public async Task<IEventArgs> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);
            return workItem;
        }

        public int GetCount()
        {
            return _workItems.Count;
        }
    }
}