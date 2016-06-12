using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Community;
using Alfred.Model.Communities;
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
        public void Should_return_all_communities_when_getCommunities()
        {
            var communities = _fixture.Build<Community>().CreateMany(5);
        }
    }
}
