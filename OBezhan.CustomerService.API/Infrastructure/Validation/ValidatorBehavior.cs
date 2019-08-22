using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace OBezhan.CustomerService.API.Infrastructure.Validation
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TResponse : ServiceResponse, new()
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            List<ValidationFailure> failures = _validators
                .Select(t => t.Validate(request))
                .SelectMany(t => t.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Count == 0)
            {
                return await next();
            }

            bool hasNotFoundErrors = failures.Any(t => StatusEquals(t.CustomState, ValidatorError.NotFound));
            HttpStatusCode statusCode = hasNotFoundErrors ? HttpStatusCode.NotFound : HttpStatusCode.BadRequest;

            var serviceResponse = new TResponse {StatusCode = statusCode};

            foreach (ValidationFailure validationFailure in failures)
            {
                serviceResponse.Errors.AddError(validationFailure.PropertyName, validationFailure.ErrorCode, validationFailure.ErrorMessage);
            }

            return serviceResponse;
        }

        private bool StatusEquals(object state, ValidatorError validatorError)
        {
            return state is ValidatorError error && error == validatorError;
        }
    }

    public enum ValidatorError
    {
        NotFound,
        BadRequest,
    }
}
