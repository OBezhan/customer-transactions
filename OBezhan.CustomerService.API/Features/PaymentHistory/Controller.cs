using MediatR;
using Microsoft.AspNetCore.Mvc;
using OBezhan.CustomerService.API.Infrastructure.Validation;
using System.Threading.Tasks;

namespace OBezhan.CustomerService.API.Features.PaymentHistory
{
    public class Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/customers/{id:long?}")]
        [ProducesResponseType(typeof(Response), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> Get(Request request)
        {
            ServiceResponse<Response> serviceResponse = await _mediator.Send(request);
            if (serviceResponse.IsSuccess)
            {
                return Ok(serviceResponse.Response);
            }
            
            return new ObjectResult(serviceResponse.Errors)
            {
                StatusCode = (int)serviceResponse.StatusCode
            };
        }
    }
}
