using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Models.Communities;

namespace Alfred.Services
{
    public interface ICommunityService
    {
        Task<IEnumerable<CommunityModel>> GetCommunities();
        Task<CommunityModel> GetCommunity(int id);
        Task<int> CreateCommunity(CreateCommunityModel createMemberModel);
        Task<CommunityModel> UpdateCommunity(UpdateCommunityModel updateCommunityModel);
        Task<bool> DeleteCommunity(int id);
    }
}
