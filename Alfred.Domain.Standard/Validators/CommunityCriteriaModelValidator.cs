using Alfred.Standard.Models.Communities;
using FluentValidation;

namespace Alfred.Domain.Standard.Validators
{
    public class CommunityCriteriaModelValidator : AbstractValidator<CommunityCriteriaModel>
    {
        public CommunityCriteriaModelValidator(IdsValidator idsValidator)
        {
            RuleFor(criteria => criteria.Ids).SetCollectionValidator(idsValidator);
            RuleFor(criteria => criteria.Email).EmailAddress();
            RuleFor(criteria => criteria.PageSize).InclusiveBetween(1, 50);
            RuleFor(criteria => criteria.Page).GreaterThanOrEqualTo(1);
        }
    }
}
