using System.Linq;
using System.Threading.Tasks;
using Alfred.Domain.Repositories;
using Alfred.Domain.Services;
using Alfred.Models.Communities;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Alfred.Domain.Tests.Services
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
            var communities = _fixture.Build<CommunityModel>()
                .CreateMany(5)
                .ToList();
            var communityRepo = Substitute.For<ICommunityRepository>();

            communityRepo.GetCommunities().Returns(communities);
            var communityService = new CommunityService(communityRepo);
            var results = communityService.GetCommunities().Result.ToList();
            results.FirstOrDefault().Should().BeOfType<CommunityModel>();
            results.Count.Should().Be(communities.Count);
        }

        [Test]
        public void Should_return_community_with_id_2_when_get_community_using_id_2()
        {
            var communities = _fixture.Build<CommunityModel>()
                .CreateMany(5);

            var communityWithIdTwo = _fixture.Build<CommunityModel>()
                .With(x => x.Id, 2)
                .Create();

            communities.ToList().Add(communityWithIdTwo);
            var communityRepo = Substitute.For<ICommunityRepository>();

            communityRepo.GetCommunity(Arg.Is(2)).Returns(communityWithIdTwo);
            var communityService = new CommunityService(communityRepo);
            var result = communityService.GetCommunity(2).Result;
            result.Should().BeOfType<CommunityModel>();
            result.Name.Should().Be(communityWithIdTwo.Name);
        }

        [Test]
        public void Should_return_null_when_get_community_using_id_2_is_not_found()
        {
            var communityRepo = Substitute.For<ICommunityRepository>();
            communityRepo.GetCommunity(Arg.Is(2)).Returns(Task.FromResult<CommunityModel>(null));

            var communityService = new CommunityService(communityRepo);
            var result = communityService.GetCommunity(2).Result;
            result.Should().BeNull();
        }

        [Test]
        public void Should_create_community_when_community_has_valid_data()
        {
            var fakeRepo = Substitute.For<ICommunityRepository>();
            var createCommunityModel = _fixture.Build<CreateCommunityModel>().Create();

            var community = GetCommunity(createCommunityModel);
            fakeRepo.GetCommunity(Arg.Is<string>(x => x == community.Email)).ReturnsNull();
            var communityService = new CommunityService(fakeRepo);

            communityService.CreateCommunity(createCommunityModel).ConfigureAwait(false);
            fakeRepo.Received(1).GetCommunity(Arg.Is<string>(x => x == community.Email));
            fakeRepo.Received(1).SaveCommunity(Arg.Is<CreateCommunityModel>(x => x.Email == community.Email));
        }

        [Test]
        public void Should_update_community_when_community_has_valid_data()
        {
            var fakeRepo = Substitute.For<ICommunityRepository>();
            var updateCommunityModel = _fixture.Build<UpdateCommunityModel>()
                .Create();
            var communityService = new CommunityService(fakeRepo);

            communityService.UpdateCommunity(updateCommunityModel).ConfigureAwait(false);
            fakeRepo.Received(1).UpdateCommunity(Arg.Is<UpdateCommunityModel>(x => x.Id == updateCommunityModel.Id));
        }

        [Test]
        public void Should_not_update_community_when_community_is_null()
        {
            var fakeRepo = Substitute.For<ICommunityRepository>();
            
            var communityService = new CommunityService(fakeRepo);

            var res = communityService.UpdateCommunity(null).Result;
            res.Should().BeNull();
        }

        [Test]
        public void Should_delete_community_when_community_id_exists()
        {
            var community = _fixture.Build<CommunityModel>()
                .With(x => x.Id, 2)
                .Create();


            var fakeRepo = Substitute.For<ICommunityRepository>();

            fakeRepo.GetCommunity(Arg.Is(2)).Returns(community);
            var communityService = new CommunityService(fakeRepo);

            communityService.DeleteCommunity(2).ConfigureAwait(false);
            fakeRepo.Received(1).DeleteCommunity(Arg.Is(2));
        }

        [Test]
        public void Should_not_delete_community_when_community_id_does_not_exists()
        {
            var fakeRepo = Substitute.For<ICommunityRepository>();

            fakeRepo.GetCommunity(Arg.Is(2)).ReturnsNull();
            var communityService = new CommunityService(fakeRepo);

            communityService.DeleteCommunity(2).ConfigureAwait(false);
            fakeRepo.DidNotReceive().DeleteCommunity(Arg.Is(2));
        }

        private CommunityModel GetCommunity(CreateCommunityModel createCommunityModel)
        {
            return new CommunityModel
            {
                Email = createCommunityModel.Email,
                Name = createCommunityModel.Name
            };
        }
    }
}
