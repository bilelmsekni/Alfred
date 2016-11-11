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
        private readonly IArtifactRepository _artifactRepository;
        private readonly IMemberRepository _memberRepository;

        public CommunityRepository(ICommunityDao communityDao, IArtifactRepository artifactRepository, IMemberRepository memberRepository)
        {
            _communityDao = communityDao;
            _artifactRepository = artifactRepository;
            _memberRepository = memberRepository;
        }

        public async Task<IEnumerable<Community>> GetCommunities()
        {
            var communities = await _communityDao.GetCommunities().ConfigureAwait(false);
            var communityTransfor = communities.Select(TransformToCommunityEntity).ToArray();
            return await Task.WhenAll(communityTransfor).ConfigureAwait(false);
        }

        private async Task<Community> TransformToCommunityEntity(CommunityDto communityDto)
        {
            if (communityDto != null)
            {
                return new Community
                {
                    Id = communityDto.Id,
                    Email = communityDto.Email,
                    Name = communityDto.Name,
                    Artifacts = await _artifactRepository.GetCommunityArtifacts(communityDto.Id).ConfigureAwait(false),
                    Members = await _memberRepository.GetCommunityMembers(communityDto.Id).ConfigureAwait(false)
                };
            }
            return null;
        }

        public async Task<Community> GetCommunity(int id)
        {
            return await TransformToCommunityEntity(await _communityDao.GetCommunity(id).ConfigureAwait(false)).ConfigureAwait(false);
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
            return await TransformToCommunityEntity(await _communityDao.GetCommunity(email).ConfigureAwait(false)).ConfigureAwait(false);
        }

        public async Task UpdateCommunity(Community community)
        {
            await _communityDao.UpdateCommunity(TransformToCommunityDto(community)).ConfigureAwait(false);
        }
    }
}
