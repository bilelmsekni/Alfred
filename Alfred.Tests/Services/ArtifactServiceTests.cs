using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.Interfaces;
using Alfred.Model;
using Alfred.Model.Artifacts;
using Alfred.Services;
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
                .Without(x => x.Member)
                .Without(x => x.Community)
                .CreateMany(5);
            _artifactRepo.GetArtifacts().Returns(artifacts);
            _modelFactory.CreateArtifactModel(Arg.Any<Artifact>()).Returns(GetArtifactModel(artifacts.FirstOrDefault()));

            var results = _artifactService.GetArtifacts();
            results.FirstOrDefault().Should().BeOfType<ArtifactModel>();
            results.Count().Should().Be(artifacts.Count());
        }

        [Test]
        public void Should_return_artifact_with_id_2_when_get_artifact_with_id_2()
        {
            var artifactWithIdTwo = _fixture.Build<Artifact>()
                .Without(x => x.Member)
                .Without(x => x.Community)
                .With(x => x.Id, 2)
                .Create();

            _artifactRepo.GetArtifact(Arg.Is(2)).Returns(artifactWithIdTwo);
            _modelFactory.CreateArtifactModel(Arg.Is<Artifact>(x => x.Id == 2)).Returns(GetArtifactModel(artifactWithIdTwo));

            var result = _artifactService.GetArtifact(2);
            result.Should().BeOfType<ArtifactModel>();
            result.Title.Should().Be(artifactWithIdTwo.Title);
            result.Status.Should().Be(artifactWithIdTwo.Status);
        }

        [Test]
        public void Should_return_null_when_no_artifact_with_id_2_exists()
        {
            _artifactRepo.GetArtifact(Arg.Is(2)).ReturnsNull();

            var result = _artifactService.GetArtifact(2);
            result.Should().BeNull();
        }

        [Test]
        public void Should_create_artifact_when_artifact_data_is_valid()
        {
            var createArtifactModel = _fixture.Build<CreateArtifactModel>()                
                .Create();
            var artifact = GetArtifact(createArtifactModel);
            _modelFactory.CreateArtifact(Arg.Is<CreateArtifactModel>(x => x.Title == createArtifactModel.Title))
                .Returns(artifact);
            _artifactRepo.GetArtifact(Arg.Is(artifact.Title)).ReturnsNull();

            _artifactService.CreateArtifact(createArtifactModel);

            _modelFactory.Received(1)
                .CreateArtifact(Arg.Is<CreateArtifactModel>(x => x.Title == createArtifactModel.Title));
            _modelFactory.Received(1).CreateArtifactModel(Arg.Is<Artifact>(x => x.Title ==artifact.Title));
            _artifactRepo.Received(1).SaveArtifact(Arg.Is<Artifact>(x => x.Title == artifact.Title));
            _artifactRepo.Received(1).GetArtifact(Arg.Is(createArtifactModel.Title));
        }

        [Test]
        public void Should_not_create_artifact_when_artifact_title_is_already_used()
        {
            var createArtifactModel = _fixture.Build<CreateArtifactModel>()
                .Create();
            var artifact = GetArtifact(createArtifactModel);
            _modelFactory.CreateArtifact(Arg.Is<CreateArtifactModel>(x => x.Title == createArtifactModel.Title))
                .Returns(artifact);
            _artifactRepo.GetArtifact(Arg.Is(artifact.Title)).Returns(artifact);

            _artifactService.CreateArtifact(createArtifactModel);

            _modelFactory.Received(1)
                .CreateArtifact(Arg.Is<CreateArtifactModel>(x => x.Title == createArtifactModel.Title));
            _modelFactory.DidNotReceive().CreateArtifactModel(Arg.Is<Artifact>(x => x.Title == artifact.Title));
            _artifactRepo.DidNotReceive().SaveArtifact(Arg.Is<Artifact>(x => x.Title == artifact.Title));
            _artifactRepo.Received(1).GetArtifact(Arg.Is(createArtifactModel.Title));
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
