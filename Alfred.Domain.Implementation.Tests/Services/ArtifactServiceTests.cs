using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.Interfaces;
using Alfred.Domain.Implementation.Services;
using Alfred.Domain.Mappers;
using Alfred.Domain.Models.Artifacts;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Alfred.Domain.Implementation.Tests.Services
{
    [TestFixture]
    public class ArtifactServiceTests
    {
        private Fixture _fixture;
        private IModelFactory _modelFactory;
        private IArtifactRepository _artifactRepo;
        private ArtifactService _artifactService;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _modelFactory = Substitute.For<IModelFactory>();
            _artifactRepo = Substitute.For<IArtifactRepository>();
            _artifactService = new ArtifactService(_artifactRepo, _modelFactory);
        }

        [Test]
        public void Should_return_5_artifacts_when_repo_has_5_artifacts()
        {
            var artifacts = _fixture.Build<Artifact>()
                .CreateMany(5);
            _artifactRepo.GetArtifacts().Returns(artifacts);
            _modelFactory.CreateArtifactModel(Arg.Any<Artifact>()).Returns(GetArtifactModel(artifacts.FirstOrDefault()));

            var results = _artifactService.GetArtifacts().Result;
            results.FirstOrDefault().Should().BeOfType<ArtifactModel>();
            results.Count().Should().Be(artifacts.Count());
        }

        [Test]
        public void Should_return_artifact_with_id_2_when_get_artifact_with_id_2()
        {
            var artifactWithIdTwo = _fixture.Build<Artifact>()
                .With(x => x.Id, 2)
                .Create();

            _artifactRepo.GetArtifact(Arg.Is(2)).Returns(artifactWithIdTwo);
            _modelFactory.CreateArtifactModel(Arg.Is<Artifact>(x => x.Id == 2)).Returns(GetArtifactModel(artifactWithIdTwo));

            var result = _artifactService.GetArtifact(2).Result;
            result.Should().BeOfType<ArtifactModel>();
            result.Title.Should().Be(artifactWithIdTwo.Title);
            result.Status.Should().Be(artifactWithIdTwo.Status);
        }

        [Test]
        public void Should_return_null_when_no_artifact_with_id_2_exists()
        {
            _artifactRepo.GetArtifact(Arg.Is(2)).Returns(Task.FromResult<Artifact>(null));

            var result = _artifactService.GetArtifact(2);
            result.Result.Should().BeNull();
        }

        [Test]
        public void Should_create_artifact_when_artifact_data_is_valid()
        {
            var createArtifactModel = _fixture.Build<CreateArtifactModel>()
                .Create();
            var artifact = GetArtifact(createArtifactModel);
            _modelFactory.CreateArtifact(Arg.Is<CreateArtifactModel>(x => x.Title == createArtifactModel.Title))
                .Returns(artifact);

            _artifactService.CreateArtifact(createArtifactModel).ConfigureAwait(false);

            _modelFactory.Received(1)
                .CreateArtifact(Arg.Is<CreateArtifactModel>(x => x.Title == createArtifactModel.Title));
            _artifactRepo.Received(1).SaveArtifact(Arg.Is<Artifact>(x => x.Title == artifact.Title));
        }

        [Test]
        public void Should_not_create_artifact_when_artifact_can_not_be_mapped_to_artifact()
        {
            var createArtifactModel = _fixture.Build<CreateArtifactModel>()
                .Create();
            var artifact = GetArtifact(createArtifactModel);
            _modelFactory.CreateArtifact(Arg.Is<CreateArtifactModel>(x => x.Title == createArtifactModel.Title))
                .ReturnsNull();

            var result = _artifactService.CreateArtifact(createArtifactModel).Result;

            _modelFactory.Received(1)
                .CreateArtifact(Arg.Is<CreateArtifactModel>(x => x.Title == createArtifactModel.Title));
            _modelFactory.DidNotReceive().CreateArtifactModel(Arg.Is<Artifact>(x => x.Title == artifact.Title));
            _artifactRepo.DidNotReceive().SaveArtifact(Arg.Is<Artifact>(x => x.Title == artifact.Title));
            result.Should().Be(-1);
        }

        [Test]
        public void Should_update_artifact_when_artifact_data_is_valid()
        {
            var updateArtifactModel = _fixture.Build<UpdateArtifactModel>()
                .Create();
            var oldArtifact = _fixture.Create<Artifact>();
            _artifactRepo.GetArtifact(Arg.Is(updateArtifactModel.Id)).Returns(oldArtifact);
            _modelFactory.CreateArtifact(Arg.Is<UpdateArtifactModel>(x => x.Id == updateArtifactModel.Id), oldArtifact)
                .Returns(GetArtifact(updateArtifactModel));

            _artifactService.UpdateArtifact(updateArtifactModel).ConfigureAwait(false);

            _modelFactory.Received(1)
                .CreateArtifact(Arg.Is<UpdateArtifactModel>(x => x.Id == updateArtifactModel.Id), oldArtifact);
            _modelFactory.Received(1).CreateArtifactModel(Arg.Is<Artifact>(x => x.Id == updateArtifactModel.Id));
            _artifactRepo.Received(1).UpdateArtifact(Arg.Is<Artifact>(x => x.Id == updateArtifactModel.Id));
            _artifactRepo.Received(1).GetArtifact(Arg.Is(updateArtifactModel.Id));
        }


        [Test]
        public void Should_not_update_artifact_when_artifact_to_update_is_not_found()
        {
            var updateArtifactModel = _fixture.Build<UpdateArtifactModel>()
                .Create();
            var artifact = GetArtifact(updateArtifactModel);
            _artifactRepo.GetArtifact(Arg.Is(artifact.Id)).ReturnsNull();

            _artifactService.UpdateArtifact(updateArtifactModel);

            _modelFactory.DidNotReceive().CreateArtifactModel(Arg.Is<Artifact>(x => x.Id == artifact.Id));
            _artifactRepo.DidNotReceive().UpdateArtifact(Arg.Is<Artifact>(x => x.Id == artifact.Id));
            _artifactRepo.Received(1).GetArtifact(Arg.Is(updateArtifactModel.Id));
        }

        [Test]
        public void Should_delete_artifact_when_artifact_to_delete_is_found()
        {
            var artifact = _fixture.Build<Artifact>()
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

        private ArtifactModel GetArtifactModel(Artifact artifact)
        {
            return new ArtifactModel
            {
                Title = artifact.Title,
                Reward = artifact.Reward,
                Bonus = artifact.Bonus,
                Type = artifact.Type,
                Status = artifact.Status
            };
        }
    }
}
