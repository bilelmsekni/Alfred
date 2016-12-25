using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.Entities.Members;
using Alfred.Dal.Implementation.Fake.Dao;
using Alfred.Dal.Implementation.Fake.EntityDtos;
using Alfred.Dal.Implementation.Fake.Mappers;
using Alfred.Shared.Enums;
using FluentAssertions;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Alfred.Dal.Implementation.Fake.Tests
{
    [TestFixture]
    public class MemberDaoTests
    {
        private MemberDao _memberDao;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
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
            results.Should().OnlyContain(r => r.Communities.All(c => c.Id == criteria.CommunityId.Value));
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
                Role = CommunityRole.Contributor
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

        [Test]
        public void Should_map_MemberDtos_to_MemberEntities()
        {

            var memberAtCommunity3 = _fixture.Build<MemberDto>()
                .With(x => x.CommunityId, 3)
                .Create();
            var memberAtCommunity2 = _fixture.Build<MemberDto>()
                .With(x => x.Id, memberAtCommunity3.Id)
                .With(x => x.CommunityId, 2)
                .With(x => x.Email, memberAtCommunity3.Email)
                .With(x => x.FirstName, memberAtCommunity3.FirstName)
                .With(x => x.LastName, memberAtCommunity3.LastName)
                .With(x => x.Role, memberAtCommunity3.Role)
                .Create();

            var members = new List<MemberDto> { memberAtCommunity3, memberAtCommunity2 };
            var result = _memberDao.ConvertDtos(members).Result;
            result.Count().Should().Be(1);
            result.First().Email.Should().Be(memberAtCommunity3.Email);
            result.First().FirstName.Should().Be(memberAtCommunity3.FirstName);
            result.First().LastName.Should().Be(memberAtCommunity3.LastName);
            result.First().Role.Should().Be((CommunityRole)memberAtCommunity3.Role);
            result.First().Id.Should().Be(memberAtCommunity3.Id);
            result.First().Communities.Should().Contain(c => c.Id == memberAtCommunity3.CommunityId);
            result.First().Communities.Should().Contain(c => c.Id == memberAtCommunity2.CommunityId);
        }
    }
}
