using System.Collections.Generic;
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
            _response.Should().OnlyContain(l => l.Href == 1 && l.Rel == "firstPage");
        }

        [Test]
        public void Should_not_return_firstPage_when_resultCount_is_0()
        {
            var resultCount = 0;
            _response = _response.AddFirstPage(resultCount);
            _response.Should().BeEmpty();
        }

        [TestCase(20, 20, 1)]
        [TestCase(20, 10, 2)]
        [TestCase(23, 10, 3)]
        [TestCase(29, 10, 3)]
        public void Should_return_lastPage_when_resultCount_is_and_pageSize_is(int resultCount, int pageSize, int expectedPage)
        {            
            _response = _response.AddLastPage(resultCount, pageSize);
            _response.Should().OnlyContain(l => l.Href == expectedPage && l.Rel == "lastPage");
        }

        [Test]
        public void Should_not_return_lastPage_when_resultCount_is_0()
        {
            var resultCount = 0;
            _response = _response.AddLastPage(resultCount, 20);
            _response.Should().BeEmpty();
        }
        
        [TestCase(20, 10, 1, 2)]
        [TestCase(23, 10, 2, 3)]
        [TestCase(50, 10, 2, 3)]
        public void Should_return_nextPage_when_resultCount_is_and_pageSize_is_and_page_is(int resultCount, int pageSize, int page, int expectedPage)
        {
            _response = _response.AddNextPage(resultCount, pageSize, page);
            _response.Should().OnlyContain(l => l.Href == expectedPage && l.Rel == "nextPage");
        }

        [TestCase(20, 20, 1)]
        [TestCase(0, 20, 1)]
        [TestCase(50, 10, 5)]
        public void Should_not_return_lastPage_when_resultCount_is_0_or_currentPage_is_lastPage(int resultCount, int pageSize, int page)
        {            
            _response = _response.AddNextPage(resultCount, pageSize, page);
            _response.Should().BeEmpty();
        }

        [TestCase(20, 2, 1)]
        [TestCase(23, 3, 2)]
        [TestCase(50, 3, 2)]
        public void Should_return_previousPage_when_resultCount_is_and_page_is(int resultCount, int page, int expectedPage)
        {
            _response = _response.AddPreviousPage(resultCount, page);
            _response.Should().OnlyContain(l => l.Href == expectedPage && l.Rel == "previousPage");
        }

        [TestCase(20, 20, 1)]
        [TestCase(0, 20, 1)]
        [TestCase(50, 10, 1)]
        public void Should_not_return_previousPage_when_resultCount_is_0_or_currentPage_is_firstPage(int resultCount, int pageSize, int page)
        {
            _response = _response.AddPreviousPage(resultCount, page);
            _response.Should().BeEmpty();
        }
    }
}
