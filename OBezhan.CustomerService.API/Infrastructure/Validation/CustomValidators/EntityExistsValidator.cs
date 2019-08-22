using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using OBezhan.CustomerService.API.Data.Model;

namespace OBezhan.CustomerService.API.Infrastructure.Validation.CustomValidators
{
    public class EntityExistsValidator<TEntity> : AsyncValidatorBase where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly ValidatorError _validationCode;

        public EntityExistsValidator(DbContext dbContext, ValidatorError validationCode = ValidatorError.NotFound)
            : base($"{typeof(TEntity).Name} with specified id does not exists.")
        {
            _dbContext = dbContext;
            _validationCode = validationCode;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken ct)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(context.PropertyValue);
            return entity != null;
        }

        protected override ValidationFailure CreateValidationError(PropertyValidatorContext context)
        {
            ValidationFailure validationFailuer = base.CreateValidationError(context);
            validationFailuer.ErrorCode = "EntityDoesNotFound";
            validationFailuer.CustomState = _validationCode;
            return validationFailuer;
        }
    }

    public static class EntityExistsRuleBuilderExtensions
    {
        public static IRuleBuilderOptions<TItem, TValue> EntityExists<TItem, TValue, TEntity>(
            this IRuleBuilder<TItem, TValue> ruleBuilder,
            DbContext dbContext,
            ValidatorError validationCode = ValidatorError.NotFound) where TEntity : class
        {
            return ruleBuilder.SetValidator(new EntityExistsValidator<TEntity>(dbContext, validationCode));
        }
    }
}
