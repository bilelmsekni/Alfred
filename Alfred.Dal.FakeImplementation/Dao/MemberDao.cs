using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.Entities.Community;
using Alfred.Dal.Entities.Member;

namespace Alfred.Dal.FakeImplementation.Dao
{
    public class MemberDao : IMemberDao
    {
        private readonly IEnumerable<Member> _members;

        public MemberDao()
        {
            _members = new List<Member>
            {
                new Member
                {
                    Email = "momo@uib.com",
                    Id = 1,            
                    FirstName = "momo",
                    LastName = "omom",
                    Role = ComunityRole.Member
                },
                new Member
                {
                    Email = "tata@uib.com",
                    Id = 2,
                    FirstName = "tata",
                    LastName = "atat",
                    Role = ComunityRole.Leader
                },
                new Member
                {
                    Email = "koko@uib.com",
                    Id = 1,
                    FirstName = "koko",
                    LastName = "okok",
                    Role = ComunityRole.Manager
                }
            };
        }

        public IEnumerable<Member> GetMembers()
        {
            return _members;
        }

        public void SaveMember(Member member)
        {
            _members.ToList().Add(member);
        }

        public Member GetMember(string email)
        {
            return _members.FirstOrDefault(x => x.Email == email);
        }

        public Member GetMember(int id)
        {
            return _members.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateMember(Member member)
        {
                _members.ToList().RemoveAt(_members.ToList().FindIndex(x=>x.Id == member.Id));
        }

        public void DeleteMember(int id)
        {
            _members.ToList().RemoveAt(_members.ToList().FindIndex(x=>x.Id == id));
        }
    }
}
