using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.Entities.Community;
using Alfred.Dal.FakeImplementation.EntityDtos;


namespace Alfred.Dal.FakeImplementation.Dao
{
    public class MemberDao : IMemberDao
    {
        private readonly IEnumerable<MemberDto> _members;

        public MemberDao()
        {
            _members = new List<MemberDto>
            {
                new MemberDto
                {
                    Email = "KickAss@SuperHeros.com",
                    Id = 1,
                    FirstName = "Kick",
                    LastName = "Ass",
                    Role = 0
                },
                new MemberDto
                {
                    Email = "HitGirl@SuperHeros.com",
                    Id = 2,
                    FirstName = "Hit",
                    LastName = "Girl",
                    Role = 1
                },
                new MemberDto
                {
                    Email = "BigDaddy@SuperHeros.com",
                    Id = 3,
                    FirstName = "Super",
                    LastName = "Heros",
                    Role = 2                    
                }
            };
        }

        public IEnumerable<MemberDto> GetMembers()
        {
            return _members;
        }

        public void SaveMember(MemberDto member)
        {
            member.Id = _members.Count() + 1;
            _members.ToList().Add(member);
        }

        public MemberDto GetMember(string email)
        {
            return _members.FirstOrDefault(x => x.Email.ToLowerInvariant() == email.ToLowerInvariant());
        }

        public MemberDto GetMember(int id)
        {
            return _members.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateMember(MemberDto member)
        {
            _members.ToList().RemoveAt(_members.ToList().FindIndex(x => x.Id == member.Id));
            _members.ToList().Add(member);
        }

        public void DeleteMember(int id)
        {
            _members.ToList().RemoveAt(_members.ToList().FindIndex(x => x.Id == id));
        }
    }
}
