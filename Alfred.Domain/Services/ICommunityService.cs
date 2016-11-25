using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Domain.Models.Communities;

namespace Alfred.Domain.Services
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
