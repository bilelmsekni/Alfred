using Alfred.Standard.Models.Artifacts;
using FluentValidation;

namespace Alfred.Domain.Standard.Validators
{
    public class ArtifactCriteriaModelValidator : AbstractValidator<ArtifactCriteriaModel>
    {
        public ArtifactCriteriaModelValidator(IdsValidator idsValidator)
        {
            RuleFor(criteria => criteria.Ids).SetCollectionValidator(idsValidator);
            RuleFor(criteria => criteria.Type).IsInEnum();
            RuleFor(criteria => criteria.Status).IsInEnum();
            RuleFor(criteria => criteria.PageSize).InclusiveBetween(1, 50);
            RuleFor(criteria => criteria.Page).GreaterThanOrEqualTo(1);
        }
    }
}
