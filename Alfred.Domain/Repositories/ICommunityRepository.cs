using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Models.Communities;

namespace Alfred.Domain.Repositories
{
    public interface ICommunityRepository
    {
        Task<IEnumerable<CommunityModel>> GetCommunities(CommunityCriteriaModel criteriaModel);
        Task<CommunityModel> GetCommunity(int id);
        Task<int> SaveCommunity(CreateCommunityModel community);
        Task DeleteCommunity(int id);
        Task<CommunityModel> GetCommunity(string email);
        Task<CommunityModel> UpdateCommunity(UpdateCommunityModel communityUpdates);
    }
}
