using FluentValidation.Results;
using FluentValidation.Validators;
using System.Net;

namespace OBezhan.CustomerService.API.Infrastructure.Validation
{
    public static class ValidationContextExtensions
    {
        public static void AddError(this CustomContext context, string errorCode, string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            context.AddFailure(new ValidationFailure(context.PropertyName, message)
            {
                ErrorCode = errorCode,
                CustomState = statusCode
            });
        }
    }
}
