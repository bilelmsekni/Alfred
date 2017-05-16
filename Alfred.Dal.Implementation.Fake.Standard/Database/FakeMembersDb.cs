using System;
using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.Implementation.Fake.Standard.EntityDtos;
using Alfred.Shared.Standard.Enums;
using Bogus;
using Bogus.DataSets;

namespace Alfred.Dal.Implementation.Fake.Standard.Database
{
    public class FakeMembersDb
    {
        private static readonly List<string> Jobs = new List<string>
        {
            "Developer",
            "Designer",
            "Lead Developer",
            "Business Analyst",
            "QualityAssurance",
            "Scrum Master",
            "Agile Coach"
        };

        private static readonly List<string> Imgs = new List<string>
        {
            "assets/img/user1-128x128.jpg",
            "assets/img/user2-128x128.jpg",
            "assets/img/user3-128x128.jpg",
            "assets/img/user4-128x128.jpg",
            "assets/img/user5-128x128.jpg",
            "assets/img/user6-128x128.jpg",
            "assets/img/user7-128x128.jpg",
            "assets/img/user8-128x128.jpg"
        };

        public static List<MemberDto> Members { get; } = GetMembers(20);

        private static List<MemberDto> GetMembers(int nb)
        {
            var memberId = 1;

            var memberRules = new Faker<MemberDto>()
                .RuleFor(m => m.Id, f => memberId++)
                .RuleFor(m => m.CommunityId, f => f.Random.Number(1, 3))
                .RuleFor(m => m.Role, f => (int)f.PickRandom<CommunityRole>())
                .RuleFor(m => m.Gender, f => (int)f.PickRandom<Name.Gender>())
                .RuleFor(m => m.FirstName, f => f.Name.FirstName())
                .RuleFor(m => m.LastName, f => f.Name.LastName())
                .RuleFor(m => m.Email, (f, u) => u.FirstName + "." + u.LastName + "@Alfred.com")
                .RuleFor(m => m.Job, f => f.PickRandom(Jobs))
                .RuleFor(m => m.CreationDate, f => f.Date.Recent(90))
                .RuleFor(m => m.ImageUrl, (f, u) => f.PickRandom(Imgs));


            var alfred = new MemberDto
            {
                CommunityId = 1,
                Role = 1,
                Id = 1,
                FirstName = "Alfred",
                LastName = "Pennyworth",
                Email = "Alfred.Reacher@Alfred.com",
                Gender = 0,
                CreationDate = DateTime.Now,
                ImageUrl = Imgs.First(),
                Job = Jobs.First()
            };

            var members = memberRules.Generate(nb).ToList();
            members.Add(alfred);
            return members;
        }
    }
}
