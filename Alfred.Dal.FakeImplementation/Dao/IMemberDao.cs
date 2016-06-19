using System.Collections.Generic;
using Alfred.Dal.FakeImplementation.EntityDtos;

namespace Alfred.Dal.FakeImplementation.Dao
{
    public interface IMemberDao
    {
        IEnumerable<MemberDto> GetMembers();
        int SaveMember(MemberDto member);
        MemberDto GetMember(string email);
        MemberDto GetMember(int id);
        void UpdateMember(MemberDto member);
        void DeleteMember(int id);
        IEnumerable<MemberDto> GetCommunityMembers(int id);
    }
}