using System.Threading.Tasks;
using Alfred.Standard.Models.Members;

namespace Alfred.Standard.Services
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
