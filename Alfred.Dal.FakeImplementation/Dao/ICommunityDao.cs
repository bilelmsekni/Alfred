using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Dal.FakeImplementation.EntityDtos;

namespace Alfred.Dal.FakeImplementation.Dao
{
    public interface ICommunityDao
    {
        Task<IEnumerable<CommunityDto>> GetCommunities();
        Task<CommunityDto> GetCommunity(int id);
        Task<int> SaveCommunity(CommunityDto communityDto);
        Task DeleteCommunity(int id);
        Task<CommunityDto> GetCommunity(string email);
        Task UpdateCommunity(CommunityDto communityDto);
    }
}