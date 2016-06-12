using System.Collections.Generic;
using Alfred.Dal.Entities.Community;

namespace Alfred.Dal.Interfaces
{
    public interface ICommunityRepository
    {
        IEnumerable<Community> GetCommunities();
        Community GetCommunity(int id);
        void SaveCommunity(Community community);
        void DeleteCommunity(int id);
        Community GetCommunity(string email);
    }
}
