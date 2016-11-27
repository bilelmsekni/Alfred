using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Domain.Entities.Member;

namespace Alfred.Dal.Daos
{
    public interface IMemberDao
    {
        Task<IEnumerable<Member>> GetMembers(MemberCriteria criteria);
        Task<int> SaveMember(Member member);
        Task<Member> GetMember(string email);
        Task<Member> GetMember(int id);
        Task UpdateMember(Member member);
        Task DeleteMember(int id);
        Task<IEnumerable<Member>> GetCommunityMembers(int id);
    }
}