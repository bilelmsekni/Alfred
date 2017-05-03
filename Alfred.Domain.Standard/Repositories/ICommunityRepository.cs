using System.Threading.Tasks;
using Alfred.Standard.Models.Communities;

namespace Alfred.Domain.Standard.Repositories
{
    public interface ICommunityRepository
    {
        Task<CommunityResponseModel> GetCommunities(CommunityCriteriaModel criteriaModel);
        Task<CommunityModel> GetCommunity(int id);
        Task<int> SaveCommunity(CreateCommunityModel community);
        Task DeleteCommunity(int id);
        Task<CommunityModel> GetCommunity(string email);
        Task<CommunityModel> UpdateCommunity(UpdateCommunityModel communityUpdates);
    }
}
