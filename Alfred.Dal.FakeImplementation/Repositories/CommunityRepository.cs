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
        private IArtifactRepository _artifactRepository;
        private IMemberRepository _memberRepository;

        public CommunityRepository(ICommunityDao communityDao, IArtifactRepository artifactRepository, IMemberRepository memberRepository)
        {
            _communityDao = communityDao;
            _artifactRepository = artifactRepository;
            _memberRepository = memberRepository;
        }

        public IEnumerable<Community> GetCommunities()
        {
            return _communityDao.GetCommunities().Select(TransformToCommunityEntity);
        }

        private Community TransformToCommunityEntity(CommunityDto communityDto)
        {
            return new Community
            {
                Id = communityDto.Id,
                Email = communityDto.Email,
                Name = communityDto.Name,
                Artifacts = _artifactRepository.GetCommunityArtifacts(communityDto.Id),
                Members = _memberRepository.GetCommunityMembers(communityDto.Id)
            };
        }

        public Community GetCommunity(int id)
        {
            return TransformToCommunityEntity(_communityDao.GetCommunity(id));
        }

        public void SaveCommunity(Community community)
        {
            _communityDao.SaveCommunity(TransformToCommunityDto(community));
        }

        private CommunityDto TransformToCommunityDto(Community community)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommunity(int id)
        {
            _communityDao.DeleteCommunity(id);
        }

        public Community GetCommunity(string email)
        {
            return TransformToCommunityEntity(_communityDao.GetCommunity(email));
        }

        public void UpdateCommunity(Community community)
        {
            _communityDao.UpdateCommunity(TransformToCommunityDto(community));
        }
    }
}
