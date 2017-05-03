using System.Threading.Tasks;
using Alfred.Standard.Models.Communities;

namespace Alfred.Standard.Services
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
