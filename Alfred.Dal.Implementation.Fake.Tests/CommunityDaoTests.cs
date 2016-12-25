using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.Entities.Communities;
using Alfred.Dal.Implementation.Fake.Dao;
using Alfred.Dal.Implementation.Fake.Mappers;
using FluentAssertions;
using NUnit.Framework;

namespace Alfred.Dal.Implementation.Fake.Tests
{
    [TestFixture]
    public class CommunityDaoTests
    {
        private CommunityDao _communityDao;

        [SetUp]
        public void Setup()
        {
            _communityDao = new CommunityDao(new EntityFactory());
        }

        [Test]
        public void Should_return_members_when_ids_are_1_2()
        {
            var criteria = new CommunityCriteria
            {
                Ids = new List<int> { 1, 2 }
            };

            var results = _communityDao.GetCommunities(criteria).Result;
            results.Should().NotBeEmpty();
            results.Should().OnlyContain(r => criteria.Ids.Contains(r.Id));
        }

        [Test]
        public void Should_not_return_members_when_ids_are_5()
        {
            var criteria = new CommunityCriteria
            {
                Ids = new List<int> { 5 }
            };

            var results = _communityDao.GetCommunities(criteria).Result;
            results.Should().BeEmpty();
        }

        [Test]
        public void Should_return_members_when_email_is_found()
        {
            var criteria = new CommunityCriteria
            {
                Email = "DotNetCommunity@superheros.com"
            };

            var results = _communityDao.GetCommunities(criteria).Result;
            results.Should().NotBeEmpty();
            results.Should().OnlyContain(r => criteria.Email == r.Email);
        }

        [Test]
        public void Should_not_return_members_when_email_is_not_found()
        {
            var criteria = new CommunityCriteria
            {
                Email = "fellowship@ofthering.com"
            };

            var results = _communityDao.GetCommunities(criteria).Result;
            results.Should().BeEmpty();
        }

        [Test]
        public void Should_return_members_when_name_is_AgileCommunity()
        {
            var criteria = new CommunityCriteria
            {
                Name = "Agile Community"
            };

            var results = _communityDao.GetCommunities(criteria).Result;
            results.Should().NotBeEmpty();
            results.Should().OnlyContain(r => r.Name.Contains(criteria.Name));
        }

        [Test]
        public void Should_not_return_members_when_name_is_Illidan()
        {
            var criteria = new CommunityCriteria
            {
                Name = "Illidan"
            };

            var results = _communityDao.GetCommunities(criteria).Result;
            results.Should().BeEmpty();
        }
    }
}
