using System.Collections.Generic;
using Alfred.Model.Members;

namespace Alfred.Services
{
    public interface IMemberService
    {
        IEnumerable<MemberModel> GetMembers();
        MemberModel GetMember(int id);
        MemberModel CreateMember(CreateMemberModel createMemberModel);
        bool DeleteMember(int id);
        MemberModel UpdateMember(UpdateMemberModel updateMemberModel);
    }
}
