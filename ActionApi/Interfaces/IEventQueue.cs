using System.Threading;
using System.Threading.Tasks;

namespace ActionApi.Interfaces
{
    public interface IEventQueue
    {
        void QueueRequestEvent(IEventArgs requestEvent);

        Task<IEventArgs> DequeueAsync(
            CancellationToken cancellationToken);

        int GetCount();
    }
}