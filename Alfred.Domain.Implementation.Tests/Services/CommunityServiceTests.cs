using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Community;
using Alfred.Dal.Repositories;
using Alfred.Domain.Implementation.Services;
using Alfred.Domain.Mappers;
using Alfred.Domain.Models.Communities;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Alfred.Domain.Implementation.Tests.Services
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
                .CreateMany(5)
                .ToList();
            var communityRepo = Substitute.For<ICommunityRepository>();
            var fakeModelFactory = Substitute.For<IModelFactory>();

            communityRepo.GetCommunities().Returns(communities);
            fakeModelFactory.CreateCommunityModel(Arg.Any<Community>()).Returns(GetCommunityModel(communities.FirstOrDefault()));
            var communityService = new CommunityService(communityRepo, fakeModelFactory);
            var results = communityService.GetCommunities().Result.ToList();
            results.FirstOrDefault().Should().BeOfType<CommunityModel>();
            results.Count.Should().Be(communities.Count);
        }

        [Test]
        public void Should_return_community_with_id_2_when_get_community_using_id_2()
        {
            var communities = _fixture.Build<Community>()
                .CreateMany(5);

            var communityWithIdTwo = _fixture.Build<Community>()
                .With(x => x.Id, 2)
                .Create();

            communities.ToList().Add(communityWithIdTwo);
            var communityRepo = Substitute.For<ICommunityRepository>();
            var fakeModelFactory = Substitute.For<IModelFactory>();

            communityRepo.GetCommunity(Arg.Is(2)).Returns(communityWithIdTwo);
            fakeModelFactory.CreateCommunityModel(Arg.Any<Community>()).Returns(GetCommunityModel(communityWithIdTwo));
            var communityService = new CommunityService(communityRepo, fakeModelFactory);
            var result = communityService.GetCommunity(2).Result;
            result.Should().BeOfType<CommunityModel>();
            result.Name.Should().Be(communityWithIdTwo.Name);
        }

        [Test]
        public void Should_return_null_when_get_community_using_id_2_is_not_found()
        {
            var communityRepo = Substitute.For<ICommunityRepository>();
            communityRepo.GetCommunity(Arg.Is(2)).Returns(Task.FromResult<Community>(null));
            var fakeModelFactory = Substitute.For<IModelFactory>();

            var communityService = new CommunityService(communityRepo, fakeModelFactory);
            var result = communityService.GetCommunity(2).Result;
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

            communityService.CreateCommunity(createCommunityModel).ConfigureAwait(false);
            fakeModelFactory.Received(1).CreateCommunity(Arg.Is<CreateCommunityModel>(x => x.Email == createCommunityModel.Email));
            fakeRepo.Received(1).GetCommunity(Arg.Is<string>(x => x == community.Email));
            fakeRepo.Received(1).SaveCommunity(Arg.Is<Community>(x => x.Email == community.Email));
        }

        [Test]
        public void Should_not_create_community_when_community_email_already_used()
        {
            var fakeRepo = Substitute.For<ICommunityRepository>();
            var fakeModelFactory = Substitute.For<IModelFactory>();
            var createCommunityModel = _fixture.Build<CreateCommunityModel>().Create();

            var community = GetCommunity(createCommunityModel);
            fakeModelFactory.CreateCommunity(Arg.Any<CreateCommunityModel>()).Returns(community);
            fakeRepo.GetCommunity(Arg.Is<string>(x => x == community.Email)).Returns(community);
            var communityService = new CommunityService(fakeRepo, fakeModelFactory);

            var result = communityService.CreateCommunity(createCommunityModel).Result;
            fakeModelFactory.Received(1).CreateCommunity(Arg.Is<CreateCommunityModel>(x => x.Email == createCommunityModel.Email));
            fakeRepo.Received(1).GetCommunity(Arg.Is<string>(x => x == community.Email));
            fakeRepo.DidNotReceive().SaveCommunity(Arg.Is<Community>(x => x.Email == community.Email));
            result.Should().Be(-1);
        }

        [Test]
        public void Should_update_community_when_community_has_valid_data()
        {
            var fakeRepo = Substitute.For<ICommunityRepository>();
            var fakeModelFactory = Substitute.For<IModelFactory>();
            var updateCommunityModel = _fixture.Build<UpdateCommunityModel>()
                .Create();
            var originalCommunity = _fixture.Create<Community>();

            
            fakeModelFactory.CreateCommunity(Arg.Any<UpdateCommunityModel>(), originalCommunity).Returns(GetCommunity(updateCommunityModel));
            fakeRepo.GetCommunity(Arg.Is(updateCommunityModel.Id)).Returns(originalCommunity);
            var communityService = new CommunityService(fakeRepo, fakeModelFactory);

            communityService.UpdateCommunity(updateCommunityModel).ConfigureAwait(false);
            fakeModelFactory.Received(1).CreateCommunity(Arg.Is<UpdateCommunityModel>(x => x.Email == updateCommunityModel.Email), originalCommunity);
            fakeModelFactory.Received(1).CreateCommunityModel(Arg.Is<Community>(x => x.Email == updateCommunityModel.Email));
            fakeRepo.Received(1).GetCommunity(Arg.Is(updateCommunityModel.Id));
            fakeRepo.Received(1).UpdateCommunity(Arg.Is<Community>(x => x.Id == updateCommunityModel.Id));
        }

        [Test]
        public void Should_not_update_community_when_community_has_a_non_existant_email()
        {
            var fakeRepo = Substitute.For<ICommunityRepository>();
            var fakeModelFactory = Substitute.For<IModelFactory>();
            var updateCommunityModel = _fixture.Build<UpdateCommunityModel>()
                .Create();
            
            fakeRepo.GetCommunity(Arg.Is(updateCommunityModel.Id)).ReturnsNull();
            var communityService = new CommunityService(fakeRepo, fakeModelFactory);

            communityService.UpdateCommunity(updateCommunityModel).ConfigureAwait(false);
            fakeModelFactory.DidNotReceive().CreateCommunity(Arg.Is<UpdateCommunityModel>(x => x.Email == updateCommunityModel.Email), null);
            fakeRepo.Received(1).GetCommunity(Arg.Is(updateCommunityModel.Id));
            fakeRepo.DidNotReceive().UpdateCommunity(Arg.Is<Community>(x => x.Email == updateCommunityModel.Email));
        }

        [Test]
        public void Should_delete_community_when_community_id_exists()
        {
            var community = _fixture.Build<Community>()
                .With(x => x.Id, 2)
                .Create();


            var fakeRepo = Substitute.For<ICommunityRepository>();
            var fakeModelFactory = Substitute.For<IModelFactory>();

            fakeRepo.GetCommunity(Arg.Is(2)).Returns(community);
            var communityService = new CommunityService(fakeRepo, fakeModelFactory);

            communityService.DeleteCommunity(2).ConfigureAwait(false);
            fakeRepo.Received(1).DeleteCommunity(Arg.Is(2));
        }

        [Test]
        public void Should_not_delete_community_when_community_id_does_not_exists()
        {
            var fakeRepo = Substitute.For<ICommunityRepository>();
            var fakeModelFactory = Substitute.For<IModelFactory>();

            fakeRepo.GetCommunity(Arg.Is(2)).ReturnsNull();
            var communityService = new CommunityService(fakeRepo, fakeModelFactory);

            communityService.DeleteCommunity(2).ConfigureAwait(false);
            fakeRepo.DidNotReceive().DeleteCommunity(Arg.Is(2));
        }

        private Community GetCommunity(CreateCommunityModel createCommunityModel)
        {
            return new Community
            {
                Email = createCommunityModel.Email,
                Name = createCommunityModel.Name
            };
        }
        private Community GetCommunity(UpdateCommunityModel createCommunityModel)
        {
            return new Community
            {
                Email = createCommunityModel.Email,
                Id = createCommunityModel.Id
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
