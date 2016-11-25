using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Domain.Models.Members;

namespace Alfred.Domain.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberModel>> GetMembers();
        Task<MemberModel> GetMember(int id);
        Task<int> CreateMember(CreateMemberModel createMemberModel);
        Task<bool> DeleteMember(int id);
        Task<MemberModel> UpdateMember(UpdateMemberModel updateMemberModel);
    }
}
