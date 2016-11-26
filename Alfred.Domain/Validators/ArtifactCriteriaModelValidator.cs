using Alfred.Models;
using FluentValidation;

namespace Alfred.Domain.Validators
{
    public class ArtifactCriteriaModelValidator : AbstractValidator<ArtifactCriteriaModel>
    {
        public ArtifactCriteriaModelValidator(IdsValidator idsValidator)
        {
            RuleFor(criteria => criteria.Ids).SetCollectionValidator(idsValidator);
            RuleFor(criteria => criteria.PageSize).InclusiveBetween(1, 50);
            RuleFor(criteria => criteria.Page).GreaterThanOrEqualTo(1);
        }
    }


}
