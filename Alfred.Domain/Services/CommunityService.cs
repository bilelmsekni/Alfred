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

        public async Task<CommunityResponseModel> GetCommunities(CommunityCriteriaModel criteriaModel)
        {
            return await _communityRepo.GetCommunities(criteriaModel).ConfigureAwait(false);
        }

        public async Task<CommunityModel> GetCommunity(int id)
        {
            return await _communityRepo.GetCommunity(id).ConfigureAwait(false);
        }

        public async Task<int> CreateCommunity(CreateCommunityModel createCommunityModel)
        {
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