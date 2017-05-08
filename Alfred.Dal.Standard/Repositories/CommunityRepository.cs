using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Dal.Standard.Daos;
using Alfred.Dal.Standard.Entities.Base;
using Alfred.Dal.Standard.Entities.Communities;
using Alfred.Dal.Standard.Mappers;
using Alfred.Standard.Models.Communities;
using Alfred.Domain.Standard.Repositories;

namespace Alfred.Dal.Standard.Repositories
{
    public class CommunityRepository : ICommunityRepository
    {
        private readonly ICommunityDao _communityDao;
        private readonly IModelFactory _modelFactory;

        public CommunityRepository(ICommunityDao communityDao, IModelFactory modelFactory)
        {
            _communityDao = communityDao;
            _modelFactory = modelFactory;
        }

        public async Task<CommunityResponseModel> GetCommunities(CommunityCriteriaModel criteriaModel)
        {
            var criteria = _modelFactory.CreateCommunityCriteria(criteriaModel);
            var communitiesCount = await _communityDao.CountCommunities(criteria).ConfigureAwait(false);

            var communityResponse = new CommunityResponse
            {
                Links = new List<Link>()
                    .AddFirstPage(communitiesCount)
                    .AddLastPage(communitiesCount, criteria.PageSize)
                    .AddNextPage(communitiesCount, criteria.PageSize, criteria.Page)
                    .AddPreviousPage(communitiesCount, criteria.Page)
            };

            if (communitiesCount > 0 && criteria.Page.IsPageInRange(communitiesCount, criteria.PageSize))
            {
                communityResponse.Results = (await _communityDao.GetCommunities(criteria).ConfigureAwait(false))
                    .Paginate(criteria.Page, criteria.PageSize);
            }
            else
            {
                communityResponse.Results = new List<Community>();
            }
            

            return _modelFactory.CreateCommunityResponseModel(communityResponse);
        }

        public async Task<CommunityModel> GetCommunity(int id)
        {
            var community = await _communityDao.GetCommunity(id).ConfigureAwait(false);
            return _modelFactory.CreateCommunityModel(community);
        }

        public async Task<int> SaveCommunity(CreateCommunityModel createCommunityModel)
        {
            var community = _modelFactory.CreateCommunity(createCommunityModel);
            return await _communityDao.SaveCommunity(community).ConfigureAwait(false);
        }

        public async Task DeleteCommunity(int id)
        {
            await _communityDao.DeleteCommunity(id).ConfigureAwait(false);
        }

        public async Task<CommunityModel> GetCommunity(string email)
        {
            return _modelFactory.CreateCommunityModel(await _communityDao.GetCommunity(email).ConfigureAwait(false));
        }

        public async Task<CommunityModel> UpdateCommunity(UpdateCommunityModel communityUpdates)
        {
            var oldCommunity = await _communityDao.GetCommunity(communityUpdates.Id).ConfigureAwait(false);
            if (oldCommunity != null)
            {
                var newCommunity = _modelFactory.CreateCommunity(communityUpdates, oldCommunity);
                if (newCommunity != null)
                {
                    await _communityDao.UpdateCommunity(newCommunity).ConfigureAwait(false);
                    return _modelFactory.CreateCommunityModel(newCommunity);
                }
            }
            return null;
        }
    }
}
