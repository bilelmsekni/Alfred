using Alfred.Domain.Validators;
using Alfred.Models.Communities;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace Alfred.Domain.Tests.Validators
{
    [TestFixture]
    public class CommunityCriteriaModelValidatorTests
    {
        private CommunityCriteriaModelValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CommunityCriteriaModelValidator(new IdsValidator());
        }

        [Test]
        public void Should_not_have_validation_error_when_no_criteria()
        {
            var criteria = new CommunityCriteriaModel
            {
                Ids = null,
                Email = null,
                Name = null,
                PageSize = 20,
                Page = 1
            };
            _validator.ShouldNotHaveValidationErrorFor(x => x.Ids, criteria);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Email, criteria);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, criteria);
            _validator.ShouldNotHaveValidationErrorFor(x => x.PageSize, criteria);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Page, criteria);
        }

        [Test]
        public void Should_have_Ids_Validator_Child()
        {
            _validator.ShouldHaveChildValidator(criteria => criteria.Ids, typeof(IdsValidator));
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
    }
}
