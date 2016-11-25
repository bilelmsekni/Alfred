using System.Collections.Generic;
using Alfred.Dal.Implementation.Fake.EntityDtos;

namespace Alfred.Dal.Implementation.Fake.Database
{
    public static class FakeDatabase
    {
        public static readonly List<ArtifactDto> ArtifactData = new List<ArtifactDto>
            {
                new ArtifactDto
                {
                    Title = "Clean Code Article",
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
                    Title = "BDD Coaching",
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
                    Title = "Agile Coaching",
                    Id = 4,
                    Bonus = 0,
                    Status = 2,
                    Type = 1,
                    Reward = 520,
                    MemberId = 2,
                    CommunityId = 0
                }
            };

        public static readonly List<CommunityDto> Communities = new List<CommunityDto>
            {
                new CommunityDto
                {
                    Id = 1,
                    Email = "DotNetCommunity@superheros.com",
                    Name = "DotNetCommunity"
                },
                new CommunityDto
                {
                    Id = 2,
                    Email = "JavaCommunity@superheros.com",
                    Name = "JavaCommunity"
                },
                new CommunityDto
                {
                    Id = 3,
                    Email = "AgileCommunity@superheros.com",
                    Name = "AgileCommunity"
                }
            };

        public static readonly List<MemberDto> Members = new List<MemberDto>
            {
                new MemberDto
                {
                    Email = "KickAss@SuperHeros.com",
                    Id = 1,
                    FirstName = "Kick",
                    LastName = "Ass",
                    Role = 0,
                    CommunityIds = new List<int> { 0}
                },
                new MemberDto
                {
                    Email = "HitGirl@SuperHeros.com",
                    Id = 2,
                    FirstName = "Hit",
                    LastName = "Girl",
                    Role = 1,
                    CommunityIds = new List<int> {1, 0 }
                },
                new MemberDto
                {
                    Email = "BigDaddy@SuperHeros.com",
                    Id = 3,
                    FirstName = "Super",
                    LastName = "Heros",
                    Role = 2,
                    CommunityIds = new List<int> {1, 2 }
                }
            };
    }
}
