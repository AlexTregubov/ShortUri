namespace UriShortening.BusinessLogic.Validation
{
    using FluentValidation;
    using Models;

    public class UriFilterValidator : BaseModelValidator<UriFilter>
    {
        public UriFilterValidator()
        {
            RuleFor(x => x.CreatedById).NotNull().NotEmpty();
        }
    }
}
