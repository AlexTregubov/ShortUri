﻿namespace UriShortening.BusinessLogic.Validation
{
    public interface IValidator
    {
        void Validate<T>(T model, string errorMessage = null);

        string GetValidationError<T>(T model);
    }
}
