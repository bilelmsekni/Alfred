using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Dal.Standard.Entities.Members;

namespace Alfred.Dal.Standard.Daos
{
    public interface IMemberDao
    {
        Task<IEnumerable<Member>> GetMembers(MemberCriteria criteria);
        Task<int> SaveMember(Member member);
        Task<Member> GetMember(int id);
        Task<Member> GetMember(string email);
        Task UpdateMember(Member member);
        Task DeleteMember(int id);
        Task<int> CountMembers(MemberCriteria criteria);
    }
}