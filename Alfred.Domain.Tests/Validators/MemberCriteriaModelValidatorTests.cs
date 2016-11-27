using Alfred.Domain.Validators;
using Alfred.Models.Members;
using Alfred.Shared.Enums;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace Alfred.Domain.Tests.Validators
{
    [TestFixture]
    public class MemberCriteriaModelValidatorTests
    {
        private MemberCriteriaModelValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new MemberCriteriaModelValidator(new IdsValidator());
        }

        [Test]
        public void Should_have_Ids_Validator_Child()
        {
            _validator.ShouldHaveChildValidator(criteria => criteria.Ids, typeof(IdsValidator));
        }

        [Test]
        public void Should_validate_criteria_when_no_filter_was_specified()
        {
            var criteria = new MemberCriteriaModel
            {
                Ids = null,
                CommunityId = null,
                Email = null,
                Name = null,
                Role = null,
                PageSize = 20,
                Page = 1
            };

            _validator.ShouldNotHaveValidationErrorFor(x => x.Ids, criteria);
            _validator.ShouldNotHaveValidationErrorFor(x => x.CommunityId, criteria);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Email, criteria);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, criteria);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Role, criteria);
            _validator.ShouldNotHaveValidationErrorFor(x => x.PageSize, criteria);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Page, criteria);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Should_have_validation_errors_when_page_value_is(int page)
        {
            _validator.ShouldHaveValidationErrorFor(c => c.Page, page);
        }

        [TestCase(50)]
        [TestCase(1)]
        public void Should_not_have_validation_errors_when_page_value_is(int page)
        {
            _validator.ShouldNotHaveValidationErrorFor(c => c.Page, page);
        }

        [TestCase(51)]
        [TestCase(-1)]
        public void Should_have_validation_errors_when_pageSize_value_is(int pageSize)
        {
            _validator.ShouldHaveValidationErrorFor(c => c.PageSize, pageSize);
        }

        [TestCase(19)]
        [TestCase(9)]
        public void Should_not_have_validation_errors_when_pageSize_value_is(int pageSize)
        {
            _validator.ShouldNotHaveValidationErrorFor(c => c.PageSize, pageSize);
        }

        [TestCase("blabla")]
        [TestCase("ca@la")]
        [TestCase("blabla.com")]
        public void Should_have_validation_errors_when_email_is_not_emailFormat(string email)
        {
            _validator.ShouldHaveValidationErrorFor(c => c.Email, email);
        }

        [Test]
        public void Should_not_have_validation_errors_when_email_is_emailFormat()
        {
            _validator.ShouldNotHaveValidationErrorFor(c => c.Email, "ca@la.com");
        }

        [Test]
        public void Should_have_validation_errors_when_role_is_not_enum_valid()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.Role, (CommunityRole?)15);
        }

        [Test]
        public void Should_not_have_validation_errors_when_role_is_enum_valid()
        {
            _validator.ShouldNotHaveValidationErrorFor(c => c.Role, (CommunityRole?)0);
        }
    }
}
