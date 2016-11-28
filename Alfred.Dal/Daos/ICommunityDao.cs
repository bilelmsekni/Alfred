using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Community;

namespace Alfred.Dal.Daos
{
    public interface ICommunityDao
    {
        Task<IEnumerable<Community>> GetCommunities(CommunityCriteria criteria);
        Task<Community> GetCommunity(int id);
        Task<int> SaveCommunity(Community community);
        Task DeleteCommunity(int id);
        Task<Community> GetCommunity(string email);
        Task UpdateCommunity(Community communityDto);
    }
}