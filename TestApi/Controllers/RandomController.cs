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
        public async Task<RandomNumberDTO> GetRandomStringAsync()
        {
            var result = await _mediator.Send(new GetRandomNumberCommand());

            return result.IsSuccess ? result.Value : new RandomNumberDTO()
            {
                Error = result.Error
            };
        }

        [HttpPost]
        [Route("Notify")]
        public async Task<bool> SendNotificationAsync(NotifiyRequest request)
        {
            await _mediator.Publish(request);
            return true;
        }
    }
}