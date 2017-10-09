﻿namespace UriShortening.BusinessLogic.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentValidation.Results;
    using Enums;
    using ErrorHandling;

    public class BusinessRulesValidator : IValidator
    {
        private const ErrorCode DefaultErrorCode = ErrorCode.InvalidInput;

        private readonly Dictionary<Type, FluentValidation.IValidator> validators;

        public BusinessRulesValidator(IEnumerable<FluentValidation.IValidator> validators)
        {
            this.validators = BuildStorage(validators);
        }

        public void Validate<T>(T model, string errorMessage = null)
        {
            FluentValidation.IValidator typeValidator;

            if (validators.TryGetValue(typeof(T), out typeValidator))
            {
                var result = typeValidator.Validate(model);
                if (!result.IsValid)
                {
                    foreach (var failure in result.Errors)
                    {
                        HandleFailure(failure, errorMessage);
                    }
                }
            }
        }

        public string GetValidationError<T>(T model)
        {
            FluentValidation.IValidator typeValidator;

            if (!validators.TryGetValue(typeof(T), out typeValidator) || typeValidator == null)
            {
                return null;
            }

            var result = typeValidator.Validate(model);
            if (result.IsValid || !result.Errors.Any())
            {
                return null;
            }

            var error = result.Errors.First();

            return string.Format(error.ErrorMessage, error.FormattedMessageArguments);
        }

        private void HandleFailure(ValidationFailure failure, string errorMessage)
        {
            var errorCode = failure.CustomState as ErrorCode? ?? DefaultErrorCode;

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new BusinessException(errorCode, errorMessage);
            }

            throw new BusinessException(errorCode, failure.ErrorMessage, failure.FormattedMessageArguments);
        }

        private Dictionary<Type, FluentValidation.IValidator> BuildStorage(
            IEnumerable<FluentValidation.IValidator> collection)
        {
            return collection
                .Select(it => new
                {
                    Validator = it,
                    TargetType = GetTargetType(it)
                })
                .Where(it => it.TargetType != null)
                .GroupBy(it => it.TargetType)
                .Where(gr => gr.Any())
                .ToDictionary(it => it.Key, it => it.First().Validator);
        }

        private Type GetTargetType(FluentValidation.IValidator validator)
        {
            var genericValidatorType = validator.GetType().GetInterfaces().FirstOrDefault(x =>
                x.IsGenericType &&
                x.GetGenericTypeDefinition() == typeof(FluentValidation.IValidator<>));

            return genericValidatorType?.GetGenericArguments()[0];
        }
    }
}
