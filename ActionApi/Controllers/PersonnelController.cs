using ActionApi.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ActionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonnelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route(nameof(GetPersonnelById))]
        public async Task<IActionResult> GetPersonnelById(int id)
        {
            var result = await _mediator.Send(new GetPersonnelRequest() 
            { 
                UserId = id 
            });

            return result.IsSuccess ? Ok(result.Value) : (IActionResult)NotFound(result.Error);
        }

        [HttpGet]
        [Route(nameof(GetAllPersonnel))]
        public async Task<IActionResult> GetAllPersonnel(string nameFilter)
        {
            var personnels = await _mediator.Send(new GetAllPersonnelsRequest()
            {
                NameFilter = nameFilter
            });

            return Ok(personnels.Value);
        }

        [HttpPost]
        [Route(nameof(AddPersonnel))]
        public async Task<IActionResult> AddPersonnel(CreatePersonnelCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess ? Ok(result.Value) : (IActionResult)NotFound(result.Error);
        }

        [HttpPost]
        [Route(nameof(AddLicense))]
        public async Task<IActionResult> AddLicense(AddLicenseCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess ? Ok() : (IActionResult)NotFound(result.Error);
        }


        [HttpPut]
        [Route(nameof(EditPersonnel))]
        public async Task<IActionResult> EditPersonnel(EditPersonnelCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess ? Ok() : (IActionResult)NotFound(result.Error);
        }

        [HttpDelete]
        [Route(nameof(DeletePersonnel))]
        public async Task<IActionResult> DeletePersonnel(DeletePersonnelCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess ? Ok() : (IActionResult)NotFound(result.Error);
        }

        [HttpDelete]
        [Route(nameof(DeleteLicense))]
        public async Task<IActionResult> DeleteLicense(DeleteLicenseCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess ? Ok() : (IActionResult)NotFound(result.Error);
        }
    }
}

