using System.Collections.Generic;
using Alfred.Dal.Entities.Member;

namespace Alfred.Dal.FakeImplementation.Dao
{
    public interface IMemberDao
    {
        IEnumerable<Member> GetMembers();
        void SaveMember(Member member);
        Member GetMember(string email);
        Member GetMember(int id);
        void UpdateMember(Member member);
        void DeleteMember(int id);
    }
}