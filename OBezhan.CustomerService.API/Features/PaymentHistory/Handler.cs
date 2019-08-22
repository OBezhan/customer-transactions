using MediatR;
using Microsoft.EntityFrameworkCore;
using OBezhan.CustomerService.API.Data;
using OBezhan.CustomerService.API.Data.Model;
using OBezhan.CustomerService.API.Infrastructure.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace OBezhan.CustomerService.API.Features.PaymentHistory
{
    public class Handler : IRequestHandler<Request, ServiceResponse<Response>>
    {
        private readonly CustomersDbContext _dbContext;

        public Handler(CustomersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            Customer customer = await _dbContext.Customers
                .Include(t => t.Transactions).ThenInclude(t => t.Status)
                .Include(t => t.Transactions).ThenInclude(t => t.Currency)
                .AsNoTracking()
                .SingleAsync(t => t.Id == request.Id, cancellationToken);
                
            IEnumerable<ResponseTransaction> transactions = customer.Transactions.Select(t => new ResponseTransaction(t.Id, t.DateTimeUtc, t.Amount, t.Currency.Code, t.Status.Name));
            var response = new Response(customer.Id, customer.Name, customer.Email, customer.MobileNumber, transactions);
            return new ServiceResponse<Response>(response, HttpStatusCode.OK);
        }
    }
}
