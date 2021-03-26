using CSharpFunctionalExtensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestApi.Interfaces;
using TestApi.Models.Events;

namespace TestApi.Commands
{
    public class QueueEventCommand : IRequest<Result>
    {
        public RandomEventArgs Event { get; set; }
    }

    public class QueueEventCommandHandler : IRequestHandler<QueueEventCommand, Result>
    {
        private readonly IEventQueue _queue;

        public QueueEventCommandHandler(IEventQueue queue)
        {
            _queue = queue;
        }
        public Task<Result> Handle(QueueEventCommand request, CancellationToken cancellationToken)
        {
            _queue.QueueRequestEvent(request.Event);
            return Task.Run(Result.Success, cancellationToken);
        }
    }

}
