using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.Mappers;
using Alfred.Domain.Entities.Artifact;
using Alfred.Domain.Entities.Community;
using Alfred.Domain.Entities.Member;
using Alfred.Models.Artifacts;
using Alfred.Models.Communities;
using Alfred.Models.Members;
using Alfred.Shared.Enums;
using Alfred.Shared.Features;
using FluentAssertions;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Alfred.Dal.Tests
{
    [TestFixture]
    public class ModelFactoryTests
    {
        private Fixture _fixture;
        private ModelFactory _modelFactory;

        [SetUp]
        public void SetUp()
        {
            _modelFactory = new ModelFactory(new ObjectDifferenceManager());
            _fixture = new Fixture();
        }

        [Test]
        public void Should_map_createMemberModel_to_Member()
        {
            var createMemberModel = _fixture.Create<CreateMemberModel>();
            var result = _modelFactory.CreateMember(createMemberModel);
            result.Email.Should().Be(createMemberModel.Email);
            result.FirstName.Should().Be(createMemberModel.FirstName);
            result.LastName.Should().Be(createMemberModel.LastName);
            result.Role.Should().Be(createMemberModel.Role);
        }

        [Test]
        public void Should_fully_map_updateMemberModel_to_Member()
        {
            var updateMemberModel = _fixture.Create<UpdateMemberModel>();
            var member = _fixture.Create<Member>();

            var result = _modelFactory.CreateMember(updateMemberModel, member);

            result.Email.Should().Be(updateMemberModel.Email);
            result.FirstName.Should().Be(updateMemberModel.FirstName);
            result.LastName.Should().Be(updateMemberModel.LastName);
            result.Role.Should().Be(updateMemberModel.Role);
            result.Id.Should().Be(updateMemberModel.Id);
        }

        [Test]
        public void Should_map_MemberEntity_to_MemberModel()
        {
            var member = _fixture.Build<Member>()
                .Create();
            var result = _modelFactory.CreateMemberModel(member);
            result.Email.Should().Be(member.Email);
            result.FirstName.Should().Be(member.FirstName);
            result.LastName.Should().Be(member.LastName);
            result.Role.Should().Be(member.Role);
            result.Id.Should().Be(member.Id);
        }

        [Test]
        public void Should_map_CreateCommunityModel_to_CommunityEntity()
        {
            var createCommunityModel = _fixture.Build<CreateCommunityModel>()
                .Create();
            var result = _modelFactory.CreateCommunity(createCommunityModel);
            result.Email.Should().Be(createCommunityModel.Email);
            result.Name.Should().Be(createCommunityModel.Name);
        }

        [Test]
        public void Should_fully_map_UpdateCommunityModel_to_CommunityEntity()
        {
            var updateCommunityModel = _fixture.Build<UpdateCommunityModel>()
                .Create();
            var originalCommunity = _fixture.Create<Community>();
            var result = _modelFactory.CreateCommunity(updateCommunityModel, originalCommunity);
            result.Email.Should().Be(updateCommunityModel.Email);
            result.Name.Should().Be(updateCommunityModel.Name);
            result.Id.Should().Be(updateCommunityModel.Id);
        }

        [Test]
        public void Should_map_Community_to_CommunityModel()
        {
            var community = _fixture.Build<Community>()
                .Create();
            var result = _modelFactory.CreateCommunityModel(community);
            result.Email.Should().Be(community.Email);
            result.Name.Should().Be(community.Name);
            result.Id.Should().Be(community.Id);
        }

        [Test]
        public void Should_map_CreateArtifactModel_to_ArtifactEntity()
        {
            var createArtifactModel = _fixture.Build<CreateArtifactModel>()
                .Create();
            var result = _modelFactory.CreateArtifact(createArtifactModel);
            result.Title.Should().Be(createArtifactModel.Title);
            result.Reward.Should().Be(createArtifactModel.Reward);
            result.Bonus.Should().Be(createArtifactModel.Bonus);
            result.Status.Should().Be(ArtifactStatus.ToDo);
            result.Type.Should().Be(createArtifactModel.Type);
            result.MemberId.Should().Be(createArtifactModel.MemberId);
            result.CommunityId.Should().Be(createArtifactModel.CommunityId);
        }

        [Test]
        public void Should_fully_map_UpdateArtifactModel_to_ArtifactEntity()
        {
            var updateArtifactModel = _fixture.Build<UpdateArtifactModel>()
                .Create();
            var artifact = _fixture.Build<Artifact>()
                .Create();
            var result = _modelFactory.CreateArtifact(updateArtifactModel, artifact);
            result.Title.Should().Be(updateArtifactModel.Title);
            result.Reward.Should().Be(updateArtifactModel.Reward);
            result.Bonus.Should().Be(updateArtifactModel.Bonus);
            result.Status.Should().Be(updateArtifactModel.Status);
            result.Type.Should().Be(updateArtifactModel.Type);
            result.Id.Should().Be(updateArtifactModel.Id);
            result.MemberId.Should().Be(updateArtifactModel.MemberId);
            result.CommunityId.Should().Be(updateArtifactModel.CommunityId);
        }

        [Test]
        public void Should_map_Artifact_to_ArtifactModel()
        {
            var artifact = _fixture.Build<Artifact>()
                .Create();
            var result = _modelFactory.CreateArtifactModel(artifact);
            result.Title.Should().Be(artifact.Title);
            result.Reward.Should().Be(artifact.Reward);
            result.Bonus.Should().Be(artifact.Bonus);
            result.Status.Should().Be(artifact.Status);
            result.Type.Should().Be(artifact.Type);
            result.Id.Should().Be(artifact.Id);
            result.MemberId.Should().Be(artifact.MemberId);
            result.CommunityId.Should().Be(artifact.CommunityId);
        }

        [Test]
        public void should_map_artifactCriteriaModel_to_artifactCriteria()
        {
            var criteriaModel = _fixture.Build<ArtifactCriteriaModel>()
                .With(x=>x.Ids, new List<string> {"1","123","99"})
                .Create();

            var result = _modelFactory.CreateArtifactCrtieria(criteriaModel);
            result.Title.Should().Be(criteriaModel.Title);
            result.Type.Should().Be(criteriaModel.Type);
            result.Status.Should().Be(criteriaModel.Status);
            result.Page.Should().Be(criteriaModel.Page);
            result.PageSize.Should().Be(criteriaModel.PageSize);
            result.MemberId.Should().Be(criteriaModel.MemberId);
            result.CommunityId.Should().Be(criteriaModel.CommunityId);
            result.Ids.Should().Equal(criteriaModel.Ids.Select(int.Parse));
        }

        [Test]
        public void should_map_memberCriteriaModel_to_memberCriteria()
        {
            var criteriaModel = _fixture.Build<MemberCriteriaModel>()
                .With(x => x.Ids, new List<string> { "1", "123", "99" })
                .Create();

            var result = _modelFactory.CreateMemberCriteria(criteriaModel);
            result.Email.Should().Be(criteriaModel.Email);
            result.Role.Should().Be(criteriaModel.Role);
            result.Page.Should().Be(criteriaModel.Page);
            result.PageSize.Should().Be(criteriaModel.PageSize);
            result.Name.Should().Be(criteriaModel.Name);
            result.CommunityId.Should().Be(criteriaModel.CommunityId);
            result.Ids.Should().Equal(criteriaModel.Ids.Select(int.Parse));            
        }

        [Test]
        public void should_map_communityCriteriaModel_to_communityCriteria()
        {
            var criteriaModel = _fixture.Build<CommunityCriteriaModel>()
                .With(x => x.Ids, new List<string> { "1", "123", "99" })
                .Create();

            var result = _modelFactory.CreateCommunityCriteria(criteriaModel);
            result.Email.Should().Be(criteriaModel.Email);
            result.Page.Should().Be(criteriaModel.Page);
            result.PageSize.Should().Be(criteriaModel.PageSize);
            result.Name.Should().Be(criteriaModel.Name);
            result.Ids.Should().Equal(criteriaModel.Ids.Select(int.Parse));
        }
    }
}
