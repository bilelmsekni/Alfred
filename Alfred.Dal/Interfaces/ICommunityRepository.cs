using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Community;

namespace Alfred.Dal.Interfaces
{
    public interface ICommunityRepository
    {
        Task<IEnumerable<Community>> GetCommunities();
        Task<Community> GetCommunity(int id);
        Task<int> SaveCommunity(Community community);
        void DeleteCommunity(int id);
        Task<Community> GetCommunity(string email);
        void UpdateCommunity(Community community);
    }
}
