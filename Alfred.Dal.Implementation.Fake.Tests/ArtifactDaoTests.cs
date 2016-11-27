using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfred.Dal.Implementation.Fake.Dao;
using Alfred.Dal.Implementation.Fake.Mappers;
using Alfred.Domain.Entities.Criteria;
using Alfred.Shared.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Alfred.Dal.Implementation.Fake.Tests
{
    [TestFixture]
    public class ArtifactDaoTests
    {
        private ArtifactDao _artifactDao;
        private EntityFactory _entityFactory;

        [SetUp]
        public void Setup()
        {
            _entityFactory = new EntityFactory();
            _artifactDao = new ArtifactDao(_entityFactory);
        }

        [Test]
        public void Should_return_artifacts_when_criteria_has_ids_32_or_4()
        {
            var criteria = new ArtifactCriteria
            {
                Ids = new List<int> {32,4}
            };
            var results = _artifactDao.GetArtifacts(criteria).Result;
            results.Should().OnlyContain(e => criteria.Ids.Contains(e.Id));
        }

        [Test]
        public void Should_not_return_artifacts_when_criteria_has_ids_999()
        {
            var criteria = new ArtifactCriteria
            {
                Ids = new List<int> { 999 }
            };
            var results = _artifactDao.GetArtifacts(criteria).Result;
            results.Should().BeEmpty();
        }

        [Test]
        public void Should_not_return_artifacts_when_criteria_has_title_blabla()
        {
            var criteria = new ArtifactCriteria
            {
                Title = "blabla"
            };
            var results = _artifactDao.GetArtifacts(criteria).Result;
            results.Should().BeEmpty();
        }

        [Test]
        public void Should_not_return_artifacts_when_criteria_has_title_cleanCode()
        {
            var criteria = new ArtifactCriteria
            {
                Title = "Clean Code"
            };
            var results = _artifactDao.GetArtifacts(criteria).Result;
            results.Should().OnlyContain(res=>res.Title.Contains(criteria.Title));
        }

        [Test]
        public void Should_not_return_artifacts_when_criteria_has_type_LongArticle()
        {
            var criteria = new ArtifactCriteria
            {
                Type = ArtifactType.LongArticle
            };
            var results = _artifactDao.GetArtifacts(criteria).Result;
            results.Should().BeEmpty();
        }

        [Test]
        public void Should_return_artifacts_when_criteria_has_type_BrownBagLaunch()
        {
            var criteria = new ArtifactCriteria
            {
                Type = ArtifactType.BrownBagLaunch
            };
            var results = _artifactDao.GetArtifacts(criteria).Result;
            results.Should().OnlyContain(x=>x.Type == criteria.Type);
        }
    }
}
