using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.Entities.Base;
using FluentAssertions;
using NUnit.Framework;

namespace Alfred.Dal.Implementation.Fake.Tests
{
    [TestFixture]
    public class ResponseLinkTests
    {
        private IList<Link> _response;

        [SetUp]
        public void Setup()
        {
            _response = new List<Link>();
        }

        [Test]
        public void Should_return_firstPage_when_resultCount_is_20()
        {
            var resultCount = 20;
            _response = _response.AddFirstPage(resultCount);
            _response.Should().OnlyContain(l => l.Href == 1 && l.Rel == "FirstPage");
        }

        [Test]
        public void Should_not_return_firstPage_when_resultCount_is_0()
        {
            var resultCount = 0;
            _response = _response.AddFirstPage(resultCount);
            _response.Should().NotContain(l => l.Href == 1 && l.Rel == "FirstPage");
        }
    }
}
