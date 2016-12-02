using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.Entities.Member;
using Alfred.Dal.Implementation.Fake.Dao;
using Alfred.Dal.Implementation.Fake.Mappers;
using Alfred.Shared.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Alfred.Dal.Implementation.Fake.Tests
{
    [TestFixture]
    public class MemberDaoTests
    {
        private MemberDao _memberDao;

        [SetUp]
        public void Setup()
        {
            _memberDao = new MemberDao(new EntityFactory());
        }

        [Test]
        public void Should_return_members_when_ids_are_1_2()
        {
            var criteria = new MemberCriteria
            {
                Ids = new List<int> { 1, 2 }
            };

            var results = _memberDao.GetMembers(criteria).Result;
            results.Should().NotBeEmpty();
            results.Should().OnlyContain(r => criteria.Ids.Contains(r.Id));
        }

        [Test]
        public void Should_not_return_members_when_ids_are_555()
        {
            var criteria = new MemberCriteria
            {
                Ids = new List<int> { 555 }
            };

            var results = _memberDao.GetMembers(criteria).Result;
            results.Should().BeEmpty();
        }

        [Test]
        public void Should_return_members_when_community_id_is_3()
        {
            var criteria = new MemberCriteria
            {
                CommunityId = 3
            };

            var results = _memberDao.GetMembers(criteria).Result;
            results.Should().NotBeEmpty();
            results.Should().OnlyContain(r => r.CommunityIds.Contains(criteria.CommunityId.Value));
        }

        [Test]
        public void Should_not_return_members_when_communityid_is_55()
        {
            var criteria = new MemberCriteria
            {
                CommunityId = 55
            };

            var results = _memberDao.GetMembers(criteria).Result;
            results.Should().BeEmpty();
        }

        [Test]
        public void Should_return_members_when_email_is_found()
        {
            var criteria = new MemberCriteria
            {
                Email = "Alfred.Reacher@Alfred.com"
            };

            var results = _memberDao.GetMembers(criteria).Result;
            results.Should().NotBeEmpty();
            results.Should().OnlyContain(r => criteria.Email == r.Email);
        }

        [Test]
        public void Should_not_return_members_when_email_is_not_found()
        {
            var criteria = new MemberCriteria
            {
                Email = "moka@nostalgie.com"
            };

            var results = _memberDao.GetMembers(criteria).Result;
            results.Should().BeEmpty();
        }

        [Test]
        public void Should_return_members_when_name_is_hit()
        {
            var criteria = new MemberCriteria
            {
                Name = "Pennyworth"
            };

            var results = _memberDao.GetMembers(criteria).Result;
            results.Should().NotBeEmpty();
            results.Should().OnlyContain(r => r.LastName.Contains(criteria.Name));
        }

        [Test]
        public void Should_not_return_members_when_name_is_trnmp()
        {
            var criteria = new MemberCriteria
            {
                Name = "Trnmp"
            };

            var results = _memberDao.GetMembers(criteria).Result;
            results.Should().BeEmpty();
        }

        [Test]
        public void Should_return_members_when_role_is_member()
        {
            var criteria = new MemberCriteria
            {
                Role = CommunityRole.Member
            };

            var results = _memberDao.GetMembers(criteria).Result;
            results.Should().NotBeEmpty();
            results.Should().OnlyContain(r => r.Role == criteria.Role);
        }

        [Test]
        public void Should_not_return_members_when_Role_is_Unkonwn()
        {
            var criteria = new MemberCriteria
            {
                Role = (CommunityRole)25
            };

            var results = _memberDao.GetMembers(criteria).Result;
            results.Should().BeEmpty();
        }
    }
}
