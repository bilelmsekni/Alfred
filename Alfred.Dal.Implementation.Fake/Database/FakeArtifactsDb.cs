using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.Implementation.Fake.EntityDtos;
using Alfred.Shared.Enums;
using Bogus;

namespace Alfred.Dal.Implementation.Fake.Database
{
    public class FakeArtifactsDb
    {
        public static List<ArtifactDto> Artifacts { get; } = GetArtifacts();

        private static List<ArtifactDto> GetArtifacts()
        {
            var artifactId = 1;
            var titles = new[] { "Clean Code Article", "ElasticSearch presentation",
                "BDD Coaching", "Agile Coaching", "SOA presentation", "TDD Kata",
            "Team Building"};

            var artifactRules = new Faker<ArtifactDto>()
                .RuleFor(a => a.Id, f => artifactId++)
                .RuleFor(a => a.CommunityId, f => f.Random.Number(1, 3))
                .RuleFor(a => a.MemberId, f => f.Random.Number(1, 10))
                .RuleFor(a => a.Bonus, f => f.Random.Number(0, 100))
                .RuleFor(a => a.Reward, f => f.Random.Number(100, 1000))
                .RuleFor(a => a.Title, f => f.PickRandom(titles))
                .RuleFor(a => a.Status, f => (int)f.PickRandom<ArtifactStatus>())
                .RuleFor(a => a.Type, f => (int)f.PickRandom<ArtifactType>());


            return artifactRules.Generate(50).ToList();            
        }
    }
}
