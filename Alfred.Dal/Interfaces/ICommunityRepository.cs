using System.Collections.Generic;
using Alfred.Dal.Entities;

namespace Alfred.Dal.Interfaces
{
    public interface ICommunityRepository
    {
        IEnumerable<Community> GetCommunities();
        Community GetCommunity(int id);
        void SaveCommunity(Community community);
        void DeleteCommunity(int id);
    }
}
