using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.FakeImplementation.Database;
using Alfred.Dal.FakeImplementation.EntityDtos;


namespace Alfred.Dal.FakeImplementation.Dao
{
    public class MemberDao : IMemberDao
    {
        private readonly List<MemberDto> _members = FakeDatabase.Members;        

        public IEnumerable<MemberDto> GetMembers()
        {
            return _members;
        }

        public int SaveMember(MemberDto member)
        {
            member.Id = _members.Count + 1;
            _members.Add(member);
            return member.Id;
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
            _members.RemoveAt(_members.FindIndex(x => x.Id == member.Id));
            _members.Add(member);
        }

        public void DeleteMember(int id)
        {
            _members.RemoveAt(_members.FindIndex(x => x.Id == id));
        }

        public IEnumerable<MemberDto> GetCommunityMembers(int id)
        {
            return _members.Where(x => x.CommunityIds.Contains(id));
        }
    }
}
