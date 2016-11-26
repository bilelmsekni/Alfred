using Alfred.Domain.Validators;
using FluentAssertions;
using NUnit.Framework;

namespace Alfred.Domain.Tests.Validators
{
    [TestFixture]
    public class IdsValidatorTests
    {
        private IdsValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new IdsValidator();
        }

        [TestCase("1")]
        [TestCase("99")]
        [TestCase("230")]
        public void Should_validate_string(string id)
        {
            var results = _validator.Validate(id);
            results.IsValid.Should().BeTrue();
        }

        [TestCase("-1")]
        [TestCase("9.9")]
        [TestCase("23@")]
        [TestCase("abc")]
        public void Should_not_validate_string(string id)
        {
            var results = _validator.Validate(id);
            results.IsValid.Should().BeFalse();
        }
    }
}
