using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<CommunityModel> GetCommunities()
        {
            var communityEntities = _communityRepo.GetCommunities();
            return communityEntities.Select(x => _modelFactory.CreateCommunityModel(x));
        }
    }
}