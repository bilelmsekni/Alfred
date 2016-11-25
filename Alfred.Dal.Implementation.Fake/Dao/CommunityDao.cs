using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Implementation.Fake.Database;
using Alfred.Dal.Implementation.Fake.EntityDtos;

namespace Alfred.Dal.Implementation.Fake.Dao
{
    public class CommunityDao : ICommunityDao
    {
        private readonly List<CommunityDto> _communities = FakeDatabase.Communities;

        public async Task<IEnumerable<CommunityDto>> GetCommunities()
        {
            return await Task.Run(() => _communities).ConfigureAwait(false);
        }

        public async Task<CommunityDto> GetCommunity(int id)
        {
            return await Task.Run(()=>_communities.FirstOrDefault(x => x.Id == id)).ConfigureAwait(false);
        }

        public async Task<int> SaveCommunity(CommunityDto communityDto)
        {
            return await Task.Run(() =>
            {
                communityDto.Id = _communities.Count + 1;
                _communities.Add(communityDto);
                return communityDto.Id;
            }).ConfigureAwait(false);
        }

        public async Task DeleteCommunity(int id)
        {
            await Task.Run(()=>_communities.RemoveAt(_communities.FindIndex(x => x.Id == id))).ConfigureAwait(false);
        }

        public async Task<CommunityDto> GetCommunity(string email)
        {
            return await Task.Run(()=>_communities.FirstOrDefault(x => x.Email.ToLowerInvariant() == email.ToLowerInvariant())).ConfigureAwait(false);
        }

        public async Task UpdateCommunity(CommunityDto communityDto)
        {
            await Task.Run(() =>
            {
                _communities.RemoveAt(_communities.FindIndex(x => x.Id == communityDto.Id));
                _communities.Add(communityDto);
            }).ConfigureAwait(false);
        }
    }
}
