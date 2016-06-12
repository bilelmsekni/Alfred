using System.Collections.Generic;
using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.Entities.Community;
using Alfred.Dal.Entities.Member;
using Alfred.Model.Members;
using FluentAssertions;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Alfred.Model.Implementation.Tests
{
    [TestFixture]
    public class ModelFactoryTests
    {
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Should_map_createMemberModel_to_Member()
        {
            var createMemberModel = _fixture.Create<CreateMemberModel>();
            var modelFactory = new ModelFactory();
            var result = modelFactory.CreateMember(createMemberModel);
            result.Email.Should().Be(createMemberModel.Email);
            result.FirstName.Should().Be(createMemberModel.FirstName);
            result.LastName.Should().Be(createMemberModel.LastName);
            result.Role.Should().Be(createMemberModel.Role);
        }

        [Test]
        public void Should_map_updateMemberModel_to_Member()
        {
            var updateMemberModel = _fixture.Create<UpdateMemberModel>();
            var modelFactory = new ModelFactory();
            var result = modelFactory.CreateMember(updateMemberModel);
            result.Email.Should().Be(updateMemberModel.Email);
            result.FirstName.Should().Be(updateMemberModel.FirstName);
            result.LastName.Should().Be(updateMemberModel.LastName);
            result.Role.Should().Be(updateMemberModel.Role);
        }

        [Test]
        public void Should_map_MemberEntity_to_MemberModel()
        {
            var fakeCommunity = new List<Community>();
            var fakeArtifact = new List<Artifact>();
            var member = _fixture.Build<Member>()
                .With(x => x.Communities, fakeCommunity)
                .With(x => x.Artifacts, fakeArtifact)
                .Create();
            var modelFactory = new ModelFactory();
            var result = modelFactory.CreateMemberModel(member);
            result.Email.Should().Be(member.Email);
            result.FirstName.Should().Be(member.FirstName);
            result.LastName.Should().Be(member.LastName);
            result.Role.Should().Be(member.Role);
            //result.Communities.Should().BeSameAs(member.Communities);
            //result.Artifacts.Should().BeSameAs(member.Artifacts);
        }
    }
}
