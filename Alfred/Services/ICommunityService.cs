using System.Threading.Tasks;
using Alfred.Models.Communities;

namespace Alfred.Services
{
    public interface ICommunityService
    {
        Task<CommunityResponseModel> GetCommunities(CommunityCriteriaModel criteriaModel);
        Task<CommunityModel> GetCommunity(int id);
        Task<int> CreateCommunity(CreateCommunityModel createMemberModel);
        Task<CommunityModel> UpdateCommunity(UpdateCommunityModel updateCommunityModel);
        Task<bool> DeleteCommunity(int id);
    }
}
