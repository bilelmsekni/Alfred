using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Dal.Implementation.Fake.EntityDtos;

namespace Alfred.Dal.Implementation.Fake.Dao
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