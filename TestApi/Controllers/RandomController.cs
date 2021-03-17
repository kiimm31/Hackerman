using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestApi.Commands;
using TestApi.Notification;
using TestApi.Services;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RandomController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly CapPublishService _capPublisher;

        public RandomController(IMediator mediator, CapPublishService capPublisher)
        {
            _mediator = mediator;
            _capPublisher = capPublisher;
        }

        [HttpGet]
        [Route("Random")]
        public async Task<string> GetRandomStringAsync()
        {
            var result = await _mediator.Send(new GetRandomNumberCommand());
            return result.IsSuccess ? $"RandomString : {result.Value}" : $"ERROR : {result.Error}";
        }

        [HttpPost]
        [Route("Notify")]
        public async Task<bool> SendNotificationAsync(NotifyRequest request)
        {
            await _mediator.Publish(request);
            return true;
        }

        [HttpPost]
        [Route("Queue")]
        public async Task<bool> QueueEvent(QueueEventCommand eventRequest)
        {
            var result = await _mediator.Send(eventRequest);
            return result.IsSuccess;
        }

        [HttpPost]
        [Route("CAP")]
        public async Task<bool> PublishEvent(QueueEventCommand eventRequest)
        {
            await _capPublisher.PublishAsync(eventRequest.Event);
            
            return true;
        }
    }
}