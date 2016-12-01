using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Artifact;
using Alfred.Domain.Repositories;
using Alfred.Domain.Services;
using Alfred.Models.Artifacts;
using Alfred.Models.Base;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Alfred.Domain.Tests.Services
{
    [TestFixture]
    public class ArtifactServiceTests
    {
        private Fixture _fixture;
        private IArtifactRepository _artifactRepo;
        private ArtifactService _artifactService;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _artifactRepo = Substitute.For<IArtifactRepository>();
            _artifactService = new ArtifactService(_artifactRepo);
        }

        [Test]
        public void Should_return_5_artifacts_when_repo_has_5_artifacts()
        {
            var response = _fixture.Build<ArtifactResponseModel>()
                .With(x => x.Results, _fixture.CreateMany<ArtifactModel>(5))
                .With(x => x.Links, _fixture.CreateMany<LinkModel>(2))
                .Create();

            _artifactRepo.GetArtifacts(Arg.Any<ArtifactCriteriaModel>()).Returns(response);

            var results = _artifactService.GetArtifacts(null).Result;
            results.Results.FirstOrDefault().Should().BeOfType<ArtifactModel>();
            results.Links.FirstOrDefault().Should().BeOfType<LinkModel>();
            results.Results.Count().Should().Be(5);
            results.Links.Count().Should().Be(2);
        }

        [Test]
        public void Should_return_null_when_repo_returns_null()
        {
            _artifactRepo.GetArtifacts(Arg.Any<ArtifactCriteriaModel>()).Returns(Task.FromResult<ArtifactResponseModel>(null));

            var result = _artifactService.GetArtifacts(new ArtifactCriteriaModel());
            result.Result.Should().BeNull();
        }

        [Test]
        public void Should_return_1_artifact_when_get_artifact_return_artifact()
        {
            var response = _fixture.Build<ArtifactModel>()
                .Create();

            _artifactRepo.GetArtifact(Arg.Any<int>()).Returns(response);

            var results = _artifactService.GetArtifact(9).Result;
            results.Should().BeOfType<ArtifactModel>();
            results.Should().NotBeNull();
        }

        [Test]
        public void Should_return_null_when_get_artifact_returns_null()
        {
            _artifactRepo.GetArtifact(Arg.Any<int>()).Returns(Task.FromResult<ArtifactModel>(null));

            var result = _artifactService.GetArtifact(9);
            result.Result.Should().BeNull();
        }

        [Test]
        public void Should_create_artifact_when_artifact_data_is_valid()
        {
            var createArtifactModel = _fixture.Build<CreateArtifactModel>()
                .Create();
            var artifact = GetArtifact(createArtifactModel);


            _artifactService.CreateArtifact(createArtifactModel).ConfigureAwait(false);


            _artifactRepo.Received(1).SaveArtifact(Arg.Is<CreateArtifactModel>(x => x.Title == artifact.Title));
        }

        [Test]
        public void Should_not_create_artifact_when_artifact_is_null()
        {
            var result = _artifactService.CreateArtifact(null).Result;

            _artifactRepo.DidNotReceive().SaveArtifact(Arg.Any<CreateArtifactModel>());
            result.Should().Be(-1);
        }

        [Test]
        public void Should_update_artifact_when_artifact_data_is_valid()
        {
            var updateArtifactModel = _fixture.Build<UpdateArtifactModel>()
                .Create();
            var oldArtifact = _fixture.Create<ArtifactModel>();
            _artifactRepo.GetArtifact(Arg.Is(updateArtifactModel.Id)).Returns(oldArtifact);


            _artifactService.UpdateArtifact(updateArtifactModel).ConfigureAwait(false);

            _artifactRepo.Received(1).UpdateArtifact(Arg.Is<UpdateArtifactModel>(x => x.Id == updateArtifactModel.Id));
        }


        [Test]
        public void Should_not_update_artifact_when_artifact_to_update_is_null()
        {
            var res = _artifactService.UpdateArtifact(null).Result;
            res.Should().BeNull();
            _artifactRepo.DidNotReceive().UpdateArtifact(Arg.Any<UpdateArtifactModel>());
        }

        [Test]
        public void Should_delete_artifact_when_artifact_to_delete_is_found()
        {
            var response = _fixture.Build<ArtifactModel>()
                .Create();
            _artifactRepo.GetArtifact(Arg.Is(2)).Returns(response);

            _artifactService.DeleteArtifact(2).ConfigureAwait(false);

            _artifactRepo.Received(1).GetArtifact(Arg.Is(2));
            _artifactRepo.Received(1).DeleteArtifact(Arg.Is(2));
        }

        [Test]
        public void Should_not_delete_artifact_when_artifact_to_delete_is_not_found()
        {
            _artifactRepo.GetArtifact(Arg.Is(2)).ReturnsNull();

            _artifactService.DeleteArtifact(2).ConfigureAwait(false);

            _artifactRepo.Received(1).GetArtifact(Arg.Is(2));
            _artifactRepo.DidNotReceive().DeleteArtifact(Arg.Is(2));
        }

        private Artifact GetArtifact(CreateArtifactModel createArtifactModel)
        {
            return new Artifact
            {
                Title = createArtifactModel.Title
            };
        }
    }
}
