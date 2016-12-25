using System;
using System.Linq;
using System.Net.Http;
using Alfred.Dal.Daos;
using Alfred.Dal.Entities.Members;
using Alfred.Dal.Mappers;
using Alfred.Dal.Repositories;
using Alfred.Models.Members;
using Alfred.Shared.Features;
using Alfred.Shared.Tests;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Alfred.Dal.Tests
{
    [TestFixture]
    public class MemberRepositoryTests
    {
        private IMemberDao _memberDao;
        private ModelFactory _modelFactory;
        private MemberRepository _memberRepo;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {

            _memberDao = Substitute.For<IMemberDao>();
            Func<HttpRequestMessage> fakeFunc = () => FakeHttpMessageBuilder.CreateFakeHttpMessage();

            _modelFactory = new ModelFactory(new ObjectDifferenceManager(), fakeFunc);
            _memberRepo = new MemberRepository(_memberDao, _modelFactory);
            _fixture = new Fixture();
        }

        [Test]
        public void Should_Get_Artifacts_When_results_exist()
        {
            _memberDao.CountMembers(Arg.Any<MemberCriteria>()).ReturnsForAnyArgs(50);
            _memberDao.GetMembers(Arg.Any<MemberCriteria>())
                .ReturnsForAnyArgs(_fixture.CreateMany<Member>(50));

            var criteria = new MemberCriteriaModel
            {
                Page = 1,
                PageSize = 20
            };

            var results = _memberRepo.GetMembers(criteria).Result;
            results.Results.Should().NotBeEmpty();
            results.Results.Count().Should().Be(20);
        }

        [Test]
        public void Should_not_return_Artifacts_When_results_do_not_exist()
        {
            _memberDao.CountMembers(Arg.Any<MemberCriteria>()).ReturnsForAnyArgs(0);
            _memberDao.GetMembers(Arg.Any<MemberCriteria>())
                .ReturnsForAnyArgs(_fixture.CreateMany<Member>(50));

            var criteria = new MemberCriteriaModel
            {
                Page = 1,
                PageSize = 20
            };

            var results = _memberRepo.GetMembers(criteria).Result;
            results.Results.Should().BeEmpty();
        }

        [Test]
        public void Should_not_return_Artifacts_When_page_do_not_exist()
        {
            _memberDao.CountMembers(Arg.Any<MemberCriteria>()).ReturnsForAnyArgs(50);
            _memberDao.GetMembers(Arg.Any<MemberCriteria>())
                .ReturnsForAnyArgs(_fixture.CreateMany<Member>(50));

            var criteria = new MemberCriteriaModel
            {
                Page = 10,
                PageSize = 20
            };

            var results = _memberRepo.GetMembers(criteria).Result;
            results.Results.Should().BeEmpty();
        }
    }
}
