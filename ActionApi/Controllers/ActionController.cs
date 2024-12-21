using ActionApi.Commands;
using ActionApi.Helpers;
using ActionApi.Notification;
using ActionApi.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ActionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActionController(IMediator mediator, CapPublishService capPublisher, IHttpHelper httpHelper)
        : ControllerBase
    {
        [HttpPost]
        [Route("Fuzzy")]
        public async Task<int> Fuzzy(FuzzyStringComparisonCommand command)
        {
            var result = await mediator.Send(command);

            return result.IsSuccess ? result.Value : 0;
        }

        [HttpGet]
        [Route("Random")]
        public async Task<string> GetRandomStringAsync()
        {
            var result = await mediator.Send(new GetRandomNumberCommand());
            return result.IsSuccess ? $"RandomString : {result.Value}" : $"ERROR : {result.Error}";
        }

        [HttpPost]
        [Route("OCR")]
        public async Task<string> OcrTask(PerformOcrCommand request)
        {
            var result = await mediator.Send(request);

            return result.IsSuccess ? result.Value : result.Error;
        }

        [HttpPost]
        [Route("CAP")]
        public async Task<bool> PublishEvent(QueueEventCommand eventRequest)
        {
            await capPublisher.PublishAsync(eventRequest.Event);

            return true;
        }

        [HttpPost]
        [Route("Queue")]
        public async Task<bool> QueueEvent(QueueEventCommand eventRequest)
        {
            var result = await mediator.Send(eventRequest);
            return result.IsSuccess;
        }

        [HttpPost]
        [Route("Notify")]
        public async Task<bool> SendNotificationAsync(NotifyRequest request)
        {
            await mediator.Publish(request);
            return true;
        }

        [HttpPost]
        [Route("TryGet")]
        public async Task<IActionResult> TryGet(string address)
        {
            var response = await httpHelper.GetAsync(new System.Uri(address));

            return Ok(response);
        }

        [HttpPost]
        [Route("TryPost")]
        public async Task<IActionResult> TryPost(string address)
        {
            var response = await httpHelper.PostAsync<string>(null, new System.Uri(address));
            return Ok(response);
        }
    }
}