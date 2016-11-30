using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Models.Members;

namespace Alfred.Services
{
    public interface IMemberService
    {
        Task<MemberResponseModel> GetMembers(MemberCriteriaModel criteriaModel);
        Task<MemberModel> GetMember(int id);
        Task<int> CreateMember(CreateMemberModel createMemberModel);
        Task<bool> DeleteMember(int id);
        Task<MemberModel> UpdateMember(UpdateMemberModel updateMemberModel);
    }
}
