using System;
using System.Linq;
using System.Net.Http;
using Alfred.Dal.Daos;
using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.Mappers;
using Alfred.Dal.Repositories;
using Alfred.Models.Artifacts;
using Alfred.Shared.Features;
using Alfred.Shared.Tests;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Alfred.Dal.Tests
{
    [TestFixture]
    public class ArtifactRepositoryTests
    {
        private IArtifactDao _artifactDao;
        private IModelFactory _modelFactory;
        private ArtifactRepository _artifactRepo;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
           
            _artifactDao = Substitute.For<IArtifactDao>();
            Func<HttpRequestMessage> fakeFunc = () => FakeHttpMessageBuilder.CreateFakeHttpMessage();
                 
            _modelFactory = new ModelFactory(new ObjectDifferenceManager(), fakeFunc);
            _artifactRepo = new ArtifactRepository(_artifactDao, _modelFactory);
            _fixture = new Fixture();
        }

        [Test]
        public void Should_Get_Artifacts_When_results_exist()
        {
            _artifactDao.CountArtifact(Arg.Any<ArtifactCriteria>()).ReturnsForAnyArgs(50);
            _artifactDao.GetArtifacts(Arg.Any<ArtifactCriteria>())
                .ReturnsForAnyArgs(_fixture.CreateMany<Artifact>(50));

            var criteria = new ArtifactCriteriaModel
            {
                Page = 1,
                PageSize = 20
            };

            var results = _artifactRepo.GetArtifacts(criteria).Result;
            results.Results.Should().NotBeEmpty();
            results.Results.Count().Should().Be(20);
        }

        [Test]
        public void Should_not_return_Artifacts_When_results_do_not_exist()
        {
            _artifactDao.CountArtifact(Arg.Any<ArtifactCriteria>()).ReturnsForAnyArgs(0);
            _artifactDao.GetArtifacts(Arg.Any<ArtifactCriteria>())
                .ReturnsForAnyArgs(_fixture.CreateMany<Artifact>(50));

            var criteria = new ArtifactCriteriaModel
            {
                Page = 1,
                PageSize = 20
            };

            var results = _artifactRepo.GetArtifacts(criteria).Result;
            results.Results.Should().BeEmpty();
        }

        [Test]
        public void Should_not_return_Artifacts_When_page_do_not_exist()
        {
            _artifactDao.CountArtifact(Arg.Any<ArtifactCriteria>()).ReturnsForAnyArgs(50);
            _artifactDao.GetArtifacts(Arg.Any<ArtifactCriteria>())
                .ReturnsForAnyArgs(_fixture.CreateMany<Artifact>(50));

            var criteria = new ArtifactCriteriaModel
            {
                Page = 10,
                PageSize = 20
            };

            var results = _artifactRepo.GetArtifacts(criteria).Result;
            results.Results.Should().BeEmpty();
        }
    }
}
