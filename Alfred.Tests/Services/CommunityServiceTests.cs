using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Community;
using Alfred.Dal.Interfaces;
using Alfred.Model;
using Alfred.Model.Communities;
using Alfred.Services;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Alfred.Tests.Services
{
    public class CommunityServiceTests
    {
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Should_return_5_communities_when_get_all_Communities()
        {
            var communities = _fixture.Build<Community>()
                .Without(x => x.Artifacts)
                .Without(x => x.Members)
                .CreateMany(5);
            var communityRepo = Substitute.For<ICommunityRepository>();
            var fakeModelFactory = Substitute.For<IModelFactory>();

            communityRepo.GetCommunities().Returns(communities);
            fakeModelFactory.CreateCommunityModel(Arg.Any<Community>()).Returns(GetCommunityModel(communities.FirstOrDefault()));
            var communityService = new CommunityService(communityRepo, fakeModelFactory);
            var results = communityService.GetCommunities();
            results.FirstOrDefault().Should().BeOfType<CommunityModel>();
            results.Count().Should().Be(communities.Count());
        }

        [Test]
        public void Should_return_community_with_id_2_when_get_community_using_id_2()
        {
            var communities = _fixture.Build<Community>()
                .Without(x => x.Artifacts)
                .Without(x => x.Members)
                .CreateMany(5);

            var communityWithIdTwo = _fixture.Build<Community>()
                .With(x => x.Id, 2)
                .Without(x => x.Artifacts)
                .Without(x => x.Members)
                .Create();

            communities.ToList().Add(communityWithIdTwo);
            var communityRepo = Substitute.For<ICommunityRepository>();
            var fakeModelFactory = Substitute.For<IModelFactory>();

            communityRepo.GetCommunity(Arg.Is(2)).Returns(communityWithIdTwo);
            fakeModelFactory.CreateCommunityModel(Arg.Any<Community>()).Returns(GetCommunityModel(communityWithIdTwo));
            var communityService = new CommunityService(communityRepo, fakeModelFactory);
            var result = communityService.GetCommunity(2);
            result.Should().BeOfType<CommunityModel>();
            result.Name.Should().Be(communityWithIdTwo.Name);
        }

        [Test]
        public void Should_return_null_when_get_community_using_id_2_is_not_found()
        {
            var communityRepo = Substitute.For<ICommunityRepository>();
            communityRepo.GetCommunity(Arg.Is(2)).ReturnsNull();
            var fakeModelFactory = Substitute.For<IModelFactory>();

            var communityService = new CommunityService(communityRepo, fakeModelFactory);
            var result = communityService.GetCommunity(2);
            result.Should().BeNull();
        }

        [Test]
        public void Should_create_community_when_community_has_valid_data()
        {
            var fakeRepo = Substitute.For<ICommunityRepository>();
            var fakeModelFactory = Substitute.For<IModelFactory>();
            var createCommunityModel = _fixture.Build<CreateCommunityModel>().Create();

            var community = GetCommunity(createCommunityModel);
            fakeModelFactory.CreateCommunity(Arg.Any<CreateCommunityModel>()).Returns(community);
            fakeRepo.GetCommunity(Arg.Is<string>(x => x == community.Email)).ReturnsNull();
            var communityService = new CommunityService(fakeRepo, fakeModelFactory);

            communityService.CreateCommunity(createCommunityModel);
            fakeModelFactory.Received(1).CreateCommunity(Arg.Is<CreateCommunityModel>(x => x.Email == createCommunityModel.Email));
            fakeModelFactory.Received(1).CreateCommunityModel(Arg.Is<Community>(x => x.Email == createCommunityModel.Email));
            fakeRepo.Received(1).GetCommunity(Arg.Is<string>(x => x == community.Email));
            fakeRepo.Received(1).SaveCommunity(Arg.Is<Community>(x => x.Email == community.Email));
        }

        private Community GetCommunity(CreateCommunityModel createCommunityModel)
        {
            return new Community
            {
                Email = createCommunityModel.Email,
                Name = createCommunityModel.Name
            };
        }

        private CommunityModel GetCommunityModel(Community community)
        {
            return new CommunityModel
            {
                Name = community.Name
            };
        }
    }
}
