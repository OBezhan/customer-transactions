using System.Linq;
using System.Net;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OBezhan.CustomerService.API.Data;
using OBezhan.CustomerService.API.Data.Model;
using OBezhan.CustomerService.API.Infrastructure.Validation;
using OBezhan.CustomerService.API.Infrastructure.Validation.CustomValidators;

namespace OBezhan.CustomerService.API.Features.PaymentHistory
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator(CustomersDbContext dbContext)
        {
            RuleFor(t => t.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .LessThan(10_000_000_000)
                .EntityExists<Request, long?, Customer>(dbContext)
                .When(t => t.Id != null);

            RuleFor(t => t.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .MaximumLength(25)
                .EmailAddress()
                .When(t => t.Email != null)
                .Custom((email, context) =>
                {
                    if (email == null)
                    {
                        return;
                    }

                    Customer customer = dbContext.Customers.SingleOrDefault(t => t.Email == email);
                    if (customer == null)
                    {
                        context.AddError("EntityDoesNotExists", "Customer with provided email does not exists.", HttpStatusCode.NotFound);
                    }
                });

            RuleFor(t => t.Id)
                .NotNull()
                .WithErrorCode("NoInquiryCriteria")
                .WithMessage("No inquiry criteria")
                .When(t => t.Email == null);
        } 
    }
}
