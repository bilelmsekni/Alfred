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
            var communities = _fixture.CreateMany<CommunityDto>(3);
            var result = _entityFactory.TransformToMemberEntity(member, communities);
            result.Email.Should().Be(member.Email);
            result.FirstName.Should().Be(member.FirstName);
            result.LastName.Should().Be(member.LastName);
            result.Role.Should().Be((CommunityRole)member.Role);
            result.Id.Should().Be(member.Id);
            result.Communities.Count.Should().Be(communities.Count());
            result.Job.Should().Be(member.Job);
            result.CreationDate.Should().Be(member.CreationDate);
            result.Gender.Should().Be(member.Gender);
            result.ImageUrl.Should().Be(member.ImageUrl);
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
