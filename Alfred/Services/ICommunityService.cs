using System.Collections.Generic;
using Alfred.Model.Communities;

namespace Alfred.Services
{
    public interface ICommunityService
    {
        IEnumerable<CommunityModel> GetCommunities();
    }
}
