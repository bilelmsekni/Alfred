using System.Linq;
using Alfred.Dal.Entities.Artifacts;
using FluentAssertions;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Alfred.Dal.Tests
{
    [TestFixture]
    public class PaginationLogicTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Should_return_51_when_resultCount_is_51_and_pageSize_60_and_page_1()
        { 
            var items = _fixture.CreateMany<Artifact>(51);            
            var pageSize = 60;
            var page = 1;
            var results = items.Paginate(page, pageSize);
            results.Count().Should().Be(51);
        }

        [TestCase(1, 50, 51, 50)]
        [TestCase(3, 20, 60, 20)]
        [TestCase(3, 20, 57, 17)]
        [TestCase(5, 20, 57, 0)]
        public void Should_return_expected_when_resultCount_and_pageSize_and_page(int page, int pageSize, int count,  int expected)
        {
            var items = _fixture.CreateMany<Artifact>(count);            
            var results = items.Paginate(page, pageSize);
            results.Count().Should().Be(expected);
        }

        [TestCase(1, 50, 51)]
        [TestCase(2, 50, 51)]
        [TestCase(2, 20, 51)]
        [TestCase(4, 10, 51)]
        public void Should_return_in_range_when_resultCount_page_pageSize(int page, int pageSize, int count)
        {
            var inRange = page.IsPageInRange(pageSize, count);

            inRange.Should().BeTrue();
        }

        [TestCase(5, 20, 40)]
        [TestCase(6, 20, 100)]
        [TestCase(3, 10, 18)]
        public void Should_return_not_in_range_when_resultCount_page_pageSize(int page, int pageSize, int count)
        {

            var inRange = page.IsPageInRange(pageSize, count);

            inRange.Should().BeFalse();
        }
    }
}
