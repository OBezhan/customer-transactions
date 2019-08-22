using FluentValidation;
using OBezhan.CustomerService.API.Infrastructure.Validation;

namespace OBezhan.CustomerService.API.Features.PaymentHistory
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(t => t.Id)
                .LessThan(10_000_000_000)
                .Custom((value, context) =>
                {
                    if (value == 0)
                    {
                        context.AddError("NotFound", "Customer is not found.");
                    }
                });
        }
    }
}
