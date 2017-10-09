namespace UriShortening.BusinessLogic.Validation
{
    using FluentValidation;
    using Models;

    public class AddUriModelValidator : BaseModelValidator<AddUriModel>
    {
        public AddUriModelValidator()
        {
            RuleFor(x => x.Uri).NotNull().NotEmpty();

            RuleFor(x => x.CreatedById).NotNull().NotEmpty();
        }
    }
}
