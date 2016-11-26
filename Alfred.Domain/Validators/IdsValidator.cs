using FluentValidation;

namespace Alfred.Domain.Validators
{
    public class IdsValidator : AbstractValidator<string>
    {
        public IdsValidator()
        {
            RuleFor(s => s).Matches("^[0-9]+$").WithMessage("Ids must be an integers");
        }
    }
}
