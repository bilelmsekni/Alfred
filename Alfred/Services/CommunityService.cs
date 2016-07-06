using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Interfaces;
using Alfred.Model;
using Alfred.Model.Communities;

namespace Alfred.Services
{
    public class CommunityService : ICommunityService
    {
        private readonly ICommunityRepository _communityRepo;
        private readonly IModelFactory _modelFactory;

        public CommunityService(ICommunityRepository communityRepo, IModelFactory modelFactory)
        {
            _communityRepo = communityRepo;
            _modelFactory = modelFactory;
        }

        public async Task<IEnumerable<CommunityModel>> GetCommunities()
        {
            var communityEntities = await _communityRepo.GetCommunities();
            return communityEntities.Select(x => _modelFactory.CreateCommunityModel(x));
        }

        public async Task<CommunityModel> GetCommunity(int id)
        {
            var communityEntity = await _communityRepo.GetCommunity(id);
            if (communityEntity != null)
            {
                return _modelFactory.CreateCommunityModel(communityEntity);
            }
            return null;
        }

        public async Task<int> CreateCommunity(CreateCommunityModel createCommunityModel)
        {
            var community = _modelFactory.CreateCommunity(createCommunityModel);
            if (community != null && !IsEmailUsed(community.Email))
            {
                return await _communityRepo.SaveCommunity(community);
            }
            return -1;
        }

        public async Task<CommunityModel> UpdateCommunity(UpdateCommunityModel updateCommunityModel)
        {
            var originalCommunity = await _communityRepo.GetCommunity(updateCommunityModel.Id);
            if (originalCommunity != null)
            {
                var community = _modelFactory.CreateCommunity(updateCommunityModel, originalCommunity);
                if (community != null)
                {
                    await Task.Run(() => _communityRepo.UpdateCommunity(community));
                    return _modelFactory.CreateCommunityModel(community);
                }
            }
            return null;
        }

        public async Task<bool> DeleteCommunity(int id)
        {
            var community = await _communityRepo.GetCommunity(id);
            if (community != null)
            {
                await Task.Run(() => _communityRepo.DeleteCommunity(id));
                return true;
            }
            return false;
        }

        private bool IsEmailUsed(string email)
        {
            return _communityRepo.GetCommunity(email) != null;
        }
    }
}