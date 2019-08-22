using MediatR;
using Microsoft.AspNetCore.Mvc;
using OBezhan.CustomerService.API.Infrastructure.Validation;

namespace OBezhan.CustomerService.API.Features.PaymentHistory
{
    public class Request : IRequest<ServiceResponse<Response>>
    {
        [FromRoute]
        public long Id { get; set; }
    }
}
