using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Member;

namespace Alfred.Dal.Interfaces
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetMembers();
        Task<Member> GetMember(int id);
        Task<int> SaveMember(Member member);
        Task DeleteMember(int id);
        Task<Member> GetMember(string email);
        Task UpdateMember(Member member);
        Task<IEnumerable<Member>> GetCommunityMembers(int id);
    }
}
