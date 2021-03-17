using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TestApi.Commands;
using TestApi.Models;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RandomController : ControllerBase
    {
        private readonly ILogger<RandomController> _logger;
        private readonly IMediator _mediator;

        public RandomController(ILogger<RandomController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
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
        public async Task<bool> SendNotificationAsync(NotifiyRequest request)
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
    }
}