using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Dal.Implementation.Fake.EntityDtos;

namespace Alfred.Dal.Implementation.Fake.Dao
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