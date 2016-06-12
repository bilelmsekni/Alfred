using System.Collections.Generic;
using Alfred.Model.Communities;

namespace Alfred.Services
{
    public interface ICommunityService
    {
        IEnumerable<CommunityModel> GetCommunities();
        CommunityModel GetCommunity(int id);
        CommunityModel CreateCommunity(CreateCommunityModel createMemberModel);
        CommunityModel UpdateCommunity(UpdateCommunityModel updateCommunityModel);
        bool DeleteCommunity(int id);
    }
}
