using MediatR;
using OBezhan.CustomerService.API.Infrastructure.Validation;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace OBezhan.CustomerService.API.Features.PaymentHistory
{
    public class Handler : IRequestHandler<Request, ServiceResponse<Response>>
    {
        public Task<ServiceResponse<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            var response = new ServiceResponse<Response>(new Response
            {
                Id = request.Id
            },HttpStatusCode.OK);

            return Task.FromResult(response);
        }
    }
}
