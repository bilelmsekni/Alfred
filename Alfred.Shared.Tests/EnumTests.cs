using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfred.Shared.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Alfred.Shared.Tests
{
    [TestFixture]
    public class EnumTests
    {
        [Test]
        public void Should_artifactType_have_no_regerssion()
        {
            ((int) ArtifactType.BrownBagLaunch).Should().Be(0);
            ((int)ArtifactType.Caoching).Should().Be(1);
            ((int)ArtifactType.Presentation).Should().Be(2);
            ((int)ArtifactType.ShortArticle).Should().Be(3);
            ((int)ArtifactType.LongArticle).Should().Be(4);
        }

        [Test]
        public void Should_artifactStatus_have_no_regerssion()
        {
            ((int)ArtifactStatus.ToDo).Should().Be(0);
            ((int)ArtifactStatus.InProgress).Should().Be(1);
            ((int)ArtifactStatus.Done).Should().Be(2);
            ((int)ArtifactStatus.Pending).Should().Be(3);
            ((int)ArtifactStatus.Canceled).Should().Be(4);
        }

        [Test]
        public void Should_communityRole_have_no_regerssion()
        {
            ((int)CommunityRole.Contributor).Should().Be(0);
            ((int)CommunityRole.Leader).Should().Be(1);
            ((int)CommunityRole.Manager).Should().Be(2);    
            ((int)CommunityRole.Admin).Should().Be(3);
        }
    }
}
