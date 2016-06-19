using System.Collections.Generic;
using Alfred.Dal.FakeImplementation.EntityDtos;

namespace Alfred.Dal.FakeImplementation.Dao
{
    public interface ICommunityDao
    {
        IEnumerable<CommunityDto> GetCommunities();
        CommunityDto GetCommunity(int id);
        void SaveCommunity(CommunityDto communityDto);
        void DeleteCommunity(int id);
        CommunityDto GetCommunity(string email);
        void UpdateCommunity(CommunityDto communityDto);
    }
}