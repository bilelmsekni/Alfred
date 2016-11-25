using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Community;
using Alfred.Dal.FakeImplementation.Dao;
using Alfred.Dal.FakeImplementation.EntityDtos;
using Alfred.Dal.Interfaces;

namespace Alfred.Dal.FakeImplementation.Repositories
{
    public class CommunityRepository : ICommunityRepository
    {
        private readonly ICommunityDao _communityDao;

        public CommunityRepository(ICommunityDao communityDao)
        {
            _communityDao = communityDao;
        }

        public async Task<IEnumerable<Community>> GetCommunities()
        {
            var communities = await _communityDao.GetCommunities().ConfigureAwait(false);
            return communities.Select(TransformToCommunityEntity).ToArray();
        }

        private Community TransformToCommunityEntity(CommunityDto communityDto)
        {
            if (communityDto != null)
            {
                return new Community
                {
                    Id = communityDto.Id,
                    Email = communityDto.Email,
                    Name = communityDto.Name
                };
            }
            return null;
        }

        public async Task<Community> GetCommunity(int id)
        {
            return TransformToCommunityEntity(await _communityDao.GetCommunity(id).ConfigureAwait(false));
        }

        public async Task<int> SaveCommunity(Community community)
        {
            return await _communityDao.SaveCommunity(TransformToCommunityDto(community)).ConfigureAwait(false);
        }

        private CommunityDto TransformToCommunityDto(Community community)
        {
            if (community != null)
            {
                return new CommunityDto
                {
                    Id = community.Id,
                    Email = community.Email,
                    Name = community.Name
                };
            }
            return null;
        }

        public async Task DeleteCommunity(int id)
        {
            await _communityDao.DeleteCommunity(id).ConfigureAwait(false);
        }

        public async Task<Community> GetCommunity(string email)
        {
            return TransformToCommunityEntity(await _communityDao.GetCommunity(email).ConfigureAwait(false));
        }

        public async Task UpdateCommunity(Community community)
        {
            await _communityDao.UpdateCommunity(TransformToCommunityDto(community)).ConfigureAwait(false);
        }
    }
}
