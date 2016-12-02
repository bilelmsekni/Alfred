using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.Implementation.Fake.EntityDtos;
using Alfred.Shared.Enums;
using Bogus;

namespace Alfred.Dal.Implementation.Fake.Database
{
    public class FakeMembersDb
    {
        public static List<MemberDto> GetMembers()
        {
            var memberId = 2;
            var memberRules = new Faker<MemberDto>()
                .RuleFor(m => m.Id, f => memberId++)
                .RuleFor(m => m.CommunityId, f => f.Random.Number(1, 3))
                .RuleFor(m => m.Role, f => (int) f.PickRandom<CommunityRole>())
                .RuleFor(m => m.FirstName, f => f.Person.FirstName)
                .RuleFor(m => m.LastName, f => f.Person.LastName)
                .RuleFor(m => m.Email, (f, u) => u.FirstName + "." + u.LastName + "@Alfred.com");

            var alfred = new MemberDto
            {
                CommunityId = 1,
                Role = 1,
                Id = 1,
                FirstName = "Alfred",
                LastName = "Pennyworth",
                Email = "Alfred.Reacher@Alfred.com"
            };

            var members = memberRules.Generate(10).ToList();
            members.Add(alfred);
            return members;
        }
    }
}
