using System;
using System.Linq;
using System.Net.Http;
using Alfred.Dal.Daos;
using Alfred.Dal.Entities.Community;
using Alfred.Dal.Mappers;
using Alfred.Dal.Repositories;
using Alfred.Models.Communities;
using Alfred.Shared.Features;
using Alfred.Shared.Tests;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Alfred.Dal.Tests
{
    [TestFixture]
    public class CommunityRepositoryTests
    {
        private ICommunityDao _communityDao;
        private IModelFactory _modelFactory;
        private CommunityRepository _communityRepo;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _communityDao = Substitute.For<ICommunityDao>();
            Func<HttpRequestMessage> fakeFunc = () => FakeHttpMessageBuilder.CreateFakeHttpMessage();

            _modelFactory = new ModelFactory(new ObjectDifferenceManager(), fakeFunc);
            _communityRepo = new CommunityRepository(_communityDao, _modelFactory);
            _fixture = new Fixture();
        }

        [Test]
        public void Should_Get_Artifacts_When_results_exist()
        {
            _communityDao.CountCommunities(Arg.Any<CommunityCriteria>()).ReturnsForAnyArgs(50);
            _communityDao.GetCommunities(Arg.Any<CommunityCriteria>())
                .ReturnsForAnyArgs(_fixture.CreateMany<Community>(50));

            var criteria = new CommunityCriteriaModel
            {
                Page = 1,
                PageSize = 20
            };

            var results = _communityRepo.GetCommunities(criteria).Result;
            results.Results.Should().NotBeEmpty();
            results.Results.Count().Should().Be(20);
        }

        [Test]
        public void Should_not_return_Artifacts_When_results_do_not_exist()
        {
            _communityDao.CountCommunities(Arg.Any<CommunityCriteria>()).ReturnsForAnyArgs(0);
            _communityDao.GetCommunities(Arg.Any<CommunityCriteria>())
                .ReturnsForAnyArgs(_fixture.CreateMany<Community>(50));

            var criteria = new CommunityCriteriaModel
            {
                Page = 1,
                PageSize = 20
            };

            var results = _communityRepo.GetCommunities(criteria).Result;
            results.Results.Should().BeEmpty();
        }

        [Test]
        public void Should_not_return_Artifacts_When_page_do_not_exist()
        {
            _communityDao.CountCommunities(Arg.Any<CommunityCriteria>()).ReturnsForAnyArgs(50);
            _communityDao.GetCommunities(Arg.Any<CommunityCriteria>())
                .ReturnsForAnyArgs(_fixture.CreateMany<Community>(50));

            var criteria = new CommunityCriteriaModel
            {
                Page = 10,
                PageSize = 20
            };

            var results = _communityRepo.GetCommunities(criteria).Result;
            results.Results.Should().BeEmpty();
        }
    }
}
