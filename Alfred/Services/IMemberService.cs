using System.Collections.Generic;
using Alfred.Dal.Entities.Member;
using Alfred.Model.Members;

namespace Alfred.Services
{
    public interface IMemberService
    {
        IEnumerable<Member> GetMembers();
        Member GetMember(int id);
        MemberModel CreateMember(CreateMemberModel createMemberModel);
        bool DeleteMember(int id);
        MemberModel UpdateMember(UpdateMemberModel updateMemberModel);
    }
}
