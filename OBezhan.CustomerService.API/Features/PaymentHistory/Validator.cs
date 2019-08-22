using FluentValidation;
using OBezhan.CustomerService.API.Data;
using OBezhan.CustomerService.API.Data.Model;
using OBezhan.CustomerService.API.Infrastructure.Validation.CustomValidators;

namespace OBezhan.CustomerService.API.Features.PaymentHistory
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator(CustomersDbContext dbContext)
        {
            RuleFor(t => t.Id)
                .LessThan(10_000_000_000)
                .EntityExists<Request, long, Customer>(dbContext);
        }
    }
}
