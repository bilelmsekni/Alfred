using FluentAssertions;
using NUnit.Framework;

namespace Alfred.Configuration.Tests
{
    [TestFixture]
    public class AppSettingsProviderTests
    {
        [Test]
        public void Should_bind_to_configuration()
        {
            var settings = AppSettingsProvider.Build<LoggingConfiguration>();
            settings.DefaultOutput.Should().Be("template");
        }
    }
}
