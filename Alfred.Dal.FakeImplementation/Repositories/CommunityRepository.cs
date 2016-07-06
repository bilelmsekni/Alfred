using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return await Task.Run(() => _communityDao.GetCommunities().Select(TransformToCommunityEntity));
        }

        private Community TransformToCommunityEntity(CommunityDto communityDto)
        {
            if (communityDto != null)
            {
                return new Community
                {
                    Id = communityDto.Id,
                    Email = communityDto.Email,
                    Name = communityDto.Name,
                    Artifacts = _artifactRepository.GetCommunityArtifacts(communityDto.Id),
                    Members = _memberRepository.GetCommunityMembers(communityDto.Id).Result
                };
            }
            return null;
        }

        public async Task<Community> GetCommunity(int id)
        {
            return TransformToCommunityEntity(await Task.Run(() => _communityDao.GetCommunity(id)));
        }

        public async Task<int> SaveCommunity(Community community)
        {
            return await Task.Run(() => _communityDao.SaveCommunity(TransformToCommunityDto(community)));
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

        public void DeleteCommunity(int id)
        {
            _communityDao.DeleteCommunity(id);
        }

        public async Task<Community> GetCommunity(string email)
        {
            return TransformToCommunityEntity(await Task.Run(() => _communityDao.GetCommunity(email)));
        }

        public void UpdateCommunity(Community community)
        {
            _communityDao.UpdateCommunity(TransformToCommunityDto(community));
        }
    }
}
