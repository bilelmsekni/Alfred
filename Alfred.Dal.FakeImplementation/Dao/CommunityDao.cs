using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.FakeImplementation.EntityDtos;

namespace Alfred.Dal.FakeImplementation.Dao
{
    public class CommunityDao : ICommunityDao
    {
        private List<CommunityDto> _communities;

        public CommunityDao()
        {
            _communities = new List<CommunityDto>
            {
                new CommunityDto
                {
                    Id = 1,
                    Email = "StarTech@superheros.com",
                    Name = "StarTech"
                },
                new CommunityDto
                {
                    Id = 2,
                    Email = "StarOne@superheros.com",
                    Name = "StarOne"
                },
                new CommunityDto
                {
                    Id = 3,
                    Email = "ArgentDawn@superheros.com",
                    Name = "ArgentDawn"
                }
            };
        }

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
