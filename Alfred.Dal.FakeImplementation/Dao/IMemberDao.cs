using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Dal.FakeImplementation.EntityDtos;

namespace Alfred.Dal.FakeImplementation.Dao
{
    public interface IMemberDao
    {
        Task<IEnumerable<MemberDto>> GetMembers();
        Task<int> SaveMember(MemberDto member);
        Task<MemberDto> GetMember(string email);
        Task<MemberDto> GetMember(int id);
        Task UpdateMember(MemberDto member);
        Task DeleteMember(int id);
        Task<IEnumerable<MemberDto>> GetCommunityMembers(int id);
    }
}