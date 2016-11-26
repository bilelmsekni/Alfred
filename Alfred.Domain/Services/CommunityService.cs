using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Domain.Repositories;
using Alfred.Models.Communities;
using Alfred.Services;

namespace Alfred.Domain.Services
{
    public class CommunityService : ICommunityService
    {
        private readonly ICommunityRepository _communityRepo;

        public CommunityService(ICommunityRepository communityRepo)
        {
            _communityRepo = communityRepo;
        }

        public async Task<IEnumerable<CommunityModel>> GetCommunities()
        {
            return await _communityRepo.GetCommunities().ConfigureAwait(false);
            //return communityEntities.Select(x => _modelFactory.CreateCommunityModel(x));
        }

        public async Task<CommunityModel> GetCommunity(int id)
        {
            return await _communityRepo.GetCommunity(id).ConfigureAwait(false);
            //if (communityEntity != null)
            //{
            //    return _modelFactory.CreateCommunityModel(communityEntity);
            //}
            //return null;
        }

        public async Task<int> CreateCommunity(CreateCommunityModel createCommunityModel)
        {
            //var community = _modelFactory.CreateCommunity(createCommunityModel);
            if (createCommunityModel != null && !IsEmailUsed(createCommunityModel.Email))
            {
                return await _communityRepo.SaveCommunity(createCommunityModel).ConfigureAwait(false);
            }
            return -1;
        }

        public async Task<CommunityModel> UpdateCommunity(UpdateCommunityModel updateCommunityModel)
        {
            if (updateCommunityModel != null)
            {
                    await _communityRepo.UpdateCommunity(updateCommunityModel).ConfigureAwait(false);

            }
            //var originalCommunity = await _communityRepo.GetCommunity(updateCommunityModel.Id).ConfigureAwait(false);
            //if (originalCommunity != null)
            //{
            //    var community = _modelFactory.CreateCommunity(updateCommunityModel, originalCommunity);
            //    if (community != null)
            //    {
            //        await _communityRepo.UpdateCommunity(community).ConfigureAwait(false);
            //        return _modelFactory.CreateCommunityModel(community);
            //    }
            //}
            return null;
        }

        public async Task<bool> DeleteCommunity(int id)
        {
            var community = await _communityRepo.GetCommunity(id).ConfigureAwait(false);
            if (community != null)
            {
                await _communityRepo.DeleteCommunity(id).ConfigureAwait(false);
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