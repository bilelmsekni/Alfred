using System.Collections.Generic;
using Alfred.Dal.Entities.Member;

namespace Alfred.Dal.Interfaces
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetMembers();
        Member GetMember(int id);
        int SaveMember(Member member);
        void DeleteMember(int id);
        Member GetMember(string email);
        void UpdateMember(Member member);
        IEnumerable<Member> GetCommunityMembers(int id);
    }
}
