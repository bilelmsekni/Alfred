using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Domain.Entities.Community;

namespace Alfred.Dal.Daos
{
    public interface ICommunityDao
    {
        Task<IEnumerable<Community>> GetCommunities();
        Task<Community> GetCommunity(int id);
        Task<int> SaveCommunity(Community community);
        Task DeleteCommunity(int id);
        Task<Community> GetCommunity(string email);
        Task UpdateCommunity(Community communityDto);
    }
}