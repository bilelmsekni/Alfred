using System.Linq;
using System.Threading.Tasks;
using Alfred.Domain.Entities.Artifact;
using Alfred.Domain.Repositories;
using Alfred.Domain.Services;
using Alfred.Models.Artifacts;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Alfred.Tests.Services
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
            var artifacts = _fixture.Build<ArtifactModel>()
                .CreateMany(5).ToList();
            _artifactRepo.GetArtifacts().Returns(artifacts);

            var results = _artifactService.GetArtifacts().Result.ToList();
            results.FirstOrDefault().Should().BeOfType<ArtifactModel>();
            results.Count.Should().Be(artifacts.Count);
        }

        [Test]
        public void Should_return_artifact_with_id_2_when_get_artifact_with_id_2()
        {
            var artifactWithIdTwo = _fixture.Build<ArtifactModel>()
                .With(x => x.Id, 2)
                .Create();

            _artifactRepo.GetArtifact(Arg.Is(2)).Returns(artifactWithIdTwo);

            var result = _artifactService.GetArtifact(2).Result;
            result.Should().BeOfType<ArtifactModel>();
            result.Title.Should().Be(artifactWithIdTwo.Title);
        }

        [Test]
        public void Should_return_null_when_no_artifact_with_id_2_exists()
        {
            _artifactRepo.GetArtifact(Arg.Is(2)).Returns(Task.FromResult<ArtifactModel>(null));

            var result = _artifactService.GetArtifact(2);
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
        public void Should_not_create_artifact_when_artifact_can_not_be_mapped_to_artifact()
        {
            var createArtifactModel = _fixture.Build<CreateArtifactModel>()
                .Create();
            var artifact = GetArtifact(createArtifactModel);
   

            var result = _artifactService.CreateArtifact(createArtifactModel).Result;

            _artifactRepo.DidNotReceive().SaveArtifact(Arg.Is<CreateArtifactModel>(x => x.Title == artifact.Title));
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
            _artifactRepo.Received(1).GetArtifact(Arg.Is(updateArtifactModel.Id));
        }


        [Test]
        public void Should_not_update_artifact_when_artifact_to_update_is_not_found()
        {
            var updateArtifactModel = _fixture.Build<UpdateArtifactModel>()
                .Create();
            var artifact = GetArtifact(updateArtifactModel);
            _artifactRepo.GetArtifact(Arg.Is(artifact.Id)).ReturnsNull();

            var res = _artifactService.UpdateArtifact(updateArtifactModel).Result;
            res.Should().BeNull();
            _artifactRepo.DidNotReceive().UpdateArtifact(Arg.Is<UpdateArtifactModel>(x => x.Id == artifact.Id));
            _artifactRepo.Received(1).GetArtifact(Arg.Is(updateArtifactModel.Id));
        }

        [Test]
        public void Should_delete_artifact_when_artifact_to_delete_is_found()
        {
            var artifact = _fixture.Build<ArtifactModel>()
                .With(x => x.Id, 2)
                .Create();
            _artifactRepo.GetArtifact(Arg.Is(2)).Returns(artifact);

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

        private Artifact GetArtifact(UpdateArtifactModel updateArtifactModel)
        {
            return new Artifact
            {
                Id = updateArtifactModel.Id
            };
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
