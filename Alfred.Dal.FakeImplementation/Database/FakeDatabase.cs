using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfred.Dal.FakeImplementation.EntityDtos;

namespace Alfred.Dal.FakeImplementation.Database
{
    public static class FakeDatabase
    {
        public static List<ArtifactDto> ArtifactData = new List<ArtifactDto>
            {
                new ArtifactDto
                {
                    Title = "Clean Code Support",
                    Id = 1,
                    Bonus = 0,
                    Status = 1,
                    Type = 0,
                    Reward = 200,
                    MemberId = 0,
                    CommunityId = 1
                },
                new ArtifactDto
                {
                    Title = "ElasticSearch presentation",
                    Id = 2,
                    Bonus = 0,
                    Status = 1,
                    Type = 2,
                    Reward = 100,
                    MemberId = 1,
                    CommunityId = 0
                
                },
                new ArtifactDto
                {
                    Title = "Formation BDD",
                    Id = 3,
                    Bonus = 0,
                    Status = 2,
                    Type = 0,
                    Reward = 370,
                    MemberId = -1,
                    CommunityId = 0
                },
                new ArtifactDto
                {
                    Title = "Formation Agile",
                    Id = 3,
                    Bonus = 0,
                    Status = 2,
                    Type = 1,
                    Reward = 520,
                    MemberId = 2,
                    CommunityId = 0
                }
            };
    }
}
