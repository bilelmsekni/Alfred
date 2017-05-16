using Alfred.Standard.Models.Members;
using FluentValidation;

namespace Alfred.Domain.Standard.Validators
{
    public class MemberCriteriaModelValidator : AbstractValidator<MemberCriteriaModel>
    {
        public MemberCriteriaModelValidator(IdsValidator idsValidator)
        {
            RuleFor(criteria => criteria.Ids).SetCollectionValidator(idsValidator);
            RuleFor(criteria => criteria.Email).EmailAddress();
            RuleFor(criteria => criteria.Role).IsInEnum();
            RuleFor(criteria => criteria.PageSize).InclusiveBetween(1, 50);
            RuleFor(criteria => criteria.Page).GreaterThanOrEqualTo(1);
        }
    }
}
