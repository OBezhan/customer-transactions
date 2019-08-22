using System;
using System.Collections.Generic;

namespace OBezhan.CustomerService.API.Infrastructure.Validation
{
    public class ErrorResponse
    {
        public IDictionary<string, IList<ValidationError>> Errors { get; private set; }

        public void AddError(string propertyName, string code, string message)
        {
            AddError(propertyName, new ValidationError(code, message));
        }

        public void AddError(string propertyName, ValidationError validationError)
        {
            if (validationError == null)
            {
                throw new ArgumentNullException(nameof(validationError));
            }

            if (propertyName == null)
            {
                throw new ArgumentException(propertyName);
            }

            if (Errors == null)
            {
                Errors = new Dictionary<string, IList<ValidationError>>();
            }

            if (!Errors.ContainsKey(propertyName))
            {
                Errors.Add(propertyName, new List<ValidationError>());
            }

            Errors[propertyName].Add(validationError);
        }

    }

    public class ValidationError
    {
        public ValidationError(string code, string message)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Message = message;
        }

        public string Code { get; }
        public string Message { get; }

    }
}
