using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestApi.Models
{
    public class NotifiyRequest : INotification
    {
        public string Subject { get; set; }
        public string Message { get; set; }
    }

    public class Notifier1 : INotificationHandler<NotifiyRequest>
    {
        public Task Handle(NotifiyRequest notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Debugging from Notifier 1. Message  : {notification.Message} ");
            return Task.CompletedTask;
        }
    }

    public class Notifier2 : INotificationHandler<NotifiyRequest>
    {
        public Task Handle(NotifiyRequest notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Debugging from Notifier 2. Message  : {notification.Message} ");
            return Task.CompletedTask;
        }
    }
}