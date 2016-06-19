using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.FakeImplementation.Database;
using Alfred.Dal.FakeImplementation.EntityDtos;

namespace Alfred.Dal.FakeImplementation.Dao
{
    public class CommunityDao : ICommunityDao
    {
        private List<CommunityDto> _communities = FakeDatabase.Communities;        

        public IEnumerable<CommunityDto> GetCommunities()
        {
            return _communities;
        }

        public CommunityDto GetCommunity(int id)
        {
            return _communities.FirstOrDefault(x => x.Id == id);
        }

        public int SaveCommunity(CommunityDto communityDto)
        {
            communityDto.Id = _communities.Count + 1;
            _communities.Add(communityDto);
            return communityDto.Id;
        }

        public void DeleteCommunity(int id)
        {
            _communities.RemoveAt(_communities.FindIndex(x => x.Id == id));
        }

        public CommunityDto GetCommunity(string email)
        {
            return _communities.FirstOrDefault(x => x.Email.ToLowerInvariant() == email.ToLowerInvariant());
        }

        public void UpdateCommunity(CommunityDto communityDto)
        {
            _communities.RemoveAt(_communities.FindIndex(x => x.Id == communityDto.Id));
            _communities.Add(communityDto);
        }
    }
}
