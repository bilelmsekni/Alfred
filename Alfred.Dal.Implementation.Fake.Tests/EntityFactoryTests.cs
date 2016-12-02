using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.Implementation.Fake.EntityDtos;
using Alfred.Dal.Implementation.Fake.Mappers;
using Alfred.Shared.Enums;
using FluentAssertions;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Alfred.Dal.Implementation.Fake.Tests
{
    [TestFixture]
    public class EntityFactoryTests
    {
        private Fixture _fixture;
        private EntityFactory _entityFactory;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _entityFactory = new EntityFactory();
        }


        [Test]
        public void Should_map_MemberDto_to_MemberEntity()
        {
            var member = _fixture.Build<MemberDto>()
                .Create();
            var result = _entityFactory.TransformToMemberEntity(member);
            result.Email.Should().Be(member.Email);
            result.FirstName.Should().Be(member.FirstName);
            result.LastName.Should().Be(member.LastName);
            result.Role.Should().Be((CommunityRole)member.Role);
            result.Id.Should().Be(member.Id);
            result.CommunityIds.Should().Contain(member.CommunityId);
        }

        [Test]
        public void Should_map_MemberDtos_to_MemberEntities()
        {

            var memberAtCommunity3 = _fixture.Build<MemberDto>()
                .With(x => x.CommunityId, 3)
                .Create();
            var memberAtCommunity4 = _fixture.Build<MemberDto>()
                .With(x => x.Id, memberAtCommunity3.Id)
                .With(x => x.CommunityId, 4)
                .With(x => x.Email, memberAtCommunity3.Email)
                .With(x => x.FirstName, memberAtCommunity3.FirstName)
                .With(x => x.LastName, memberAtCommunity3.LastName)
                .With(x => x.Role, memberAtCommunity3.Role)
                .Create();

            var members = new List<MemberDto> { memberAtCommunity3, memberAtCommunity4 };
            var result = _entityFactory.TransformToMemberEntities(members);
            result.Count().Should().Be(1);
            result.First().Email.Should().Be(memberAtCommunity3.Email);
            result.First().FirstName.Should().Be(memberAtCommunity3.FirstName);
            result.First().LastName.Should().Be(memberAtCommunity3.LastName);
            result.First().Role.Should().Be((CommunityRole)memberAtCommunity3.Role);
            result.First().Id.Should().Be(memberAtCommunity3.Id);
            result.First().CommunityIds.Should().Contain(memberAtCommunity3.CommunityId);
            result.First().CommunityIds.Should().Contain(memberAtCommunity4.CommunityId);
        }

        [Test]
        public void Should_map_CommunityDto_to_CommunityEntity()
        {
            var community = _fixture.Build<CommunityDto>()
                .Create();
            var result = _entityFactory.TransformToCommunityEntity(community);
            result.Email.Should().Be(community.Email);
            result.Name.Should().Be(community.Name);
            result.Id.Should().Be(community.Id);
        }


        [Test]
        public void Should_map_ArtifactDto_to_ArtifactEntity()
        {
            var artifact = _fixture.Build<ArtifactDto>()
                .Create();
            var result = _entityFactory.TransformToArtifactEntity(artifact);
            result.Title.Should().Be(artifact.Title);
            result.Reward.Should().Be(artifact.Reward);
            result.Bonus.Should().Be(artifact.Bonus);
            result.Status.Should().Be((ArtifactStatus)artifact.Status);
            result.Type.Should().Be((ArtifactType)artifact.Type);
            result.Id.Should().Be(artifact.Id);
            result.MemberId.Should().Be(artifact.MemberId);
            result.CommunityId.Should().Be(artifact.CommunityId);
        }
    }
}
