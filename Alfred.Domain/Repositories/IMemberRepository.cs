using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Models.Members;

namespace Alfred.Domain.Repositories
{
    public interface IMemberRepository
    {
        Task<MemberResponseModel> GetMembers(MemberCriteriaModel criteriaModel);
        Task<MemberModel> GetMember(int id);
        Task<int> SaveMember(CreateMemberModel member);
        Task DeleteMember(int id);
        Task<MemberModel> GetMember(string email);
        Task<MemberModel> UpdateMember(UpdateMemberModel member);
    }
}
