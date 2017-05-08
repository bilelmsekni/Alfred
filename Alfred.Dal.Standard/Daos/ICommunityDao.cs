using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Dal.Standard.Entities.Communities;

namespace Alfred.Dal.Standard.Daos
{
    public interface ICommunityDao
    {
        Task<IEnumerable<Community>> GetCommunities(CommunityCriteria criteria);
        Task<Community> GetCommunity(int id);
        Task<int> SaveCommunity(Community community);
        Task DeleteCommunity(int id);
        Task<Community> GetCommunity(string email);
        Task UpdateCommunity(Community communityDto);
        Task<int> CountCommunities(CommunityCriteria criteria);
    }
}