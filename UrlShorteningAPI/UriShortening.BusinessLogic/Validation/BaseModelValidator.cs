namespace UriShortening.BusinessLogic.Validation
{
    using FluentValidation;

    public abstract class BaseModelValidator<T> : AbstractValidator<T>
    {
        protected BaseModelValidator()
        {
            CascadeMode = CascadeMode.Continue;
        }
    }
}
