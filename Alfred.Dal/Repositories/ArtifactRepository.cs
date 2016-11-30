using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Daos;
using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.Entities.Base;
using Alfred.Dal.Mappers;
using Alfred.Domain.Repositories;
using Alfred.Models.Artifacts;

namespace Alfred.Dal.Repositories
{
    public class ArtifactRepository : IArtifactRepository
    {
        private readonly IArtifactDao _artifactDao;
        private readonly IModelFactory _modelFactory;

        public ArtifactRepository(IArtifactDao artifactDao, IModelFactory modelFactory)
        {
            _artifactDao = artifactDao;
            _modelFactory = modelFactory;
        }

        public async Task<ArtifactResponseModel> GetArtifacts(ArtifactCriteriaModel criteriaModel)
        {
            var artifactCriteria = _modelFactory.CreateArtifactCrtieria(criteriaModel);
            var resultCount = await _artifactDao.CountArtifact(artifactCriteria).ConfigureAwait(false);

            var artifactResponse = new ArtifactResponse
            {
                Links = new List<Link>()
                    .AddFirstPage(resultCount)
                    .AddLastPage(resultCount, artifactCriteria.PageSize)
                    .AddNextPage(resultCount, artifactCriteria.PageSize, artifactCriteria.Page)
                    .AddPreviousPage(resultCount, artifactCriteria.Page)
            };

            if (resultCount > 0 && artifactCriteria.Page.IsPageInRange(resultCount, artifactCriteria.PageSize))
            {
                artifactResponse.Results = (await _artifactDao.GetArtifacts(artifactCriteria).ConfigureAwait(false))
                    .Paginate(artifactCriteria.Page, artifactCriteria.PageSize);
            }
            else
            {
                artifactResponse.Results = new List<Artifact>();
            }
            return _modelFactory.CreateArtifactResponseModel(artifactResponse);
        }        

        public async Task<ArtifactModel> GetArtifact(int id)
        {
            var artifact = await _artifactDao.GetArtifact(id).ConfigureAwait(false);
            return _modelFactory.CreateArtifactModel(artifact);
        }

        public async Task<int> SaveArtifact(CreateArtifactModel artifactModel)
        {
            var artifact = _modelFactory.CreateArtifact(artifactModel);
            return await _artifactDao.SaveArtifact(artifact).ConfigureAwait(false);
        }

        public async Task DeleteArtifact(int id)
        {
            await _artifactDao.DeleteArtifact(id).ConfigureAwait(false);
        }

        public async Task<ArtifactModel> UpdateArtifact(UpdateArtifactModel artifactUpdates)
        {
            var oldArtifact = await _artifactDao.GetArtifact(artifactUpdates.Id).ConfigureAwait(false);
            if (oldArtifact != null)
            {
                var newArtifact = _modelFactory.CreateArtifact(artifactUpdates, oldArtifact);
                if (newArtifact != null)
                {
                    await _artifactDao.UpdateArtifact(newArtifact).ConfigureAwait(false);
                    return _modelFactory.CreateArtifactModel(newArtifact);
                }
            }
            return null;
        }

        public async Task<IEnumerable<ArtifactModel>> GetMemberArtifacts(int id)
        {
            var artifactDtos = await _artifactDao.GetMemberArtifacts(id).ConfigureAwait(false);
            return artifactDtos.Select(_modelFactory.CreateArtifactModel);
        }

        public async Task<IEnumerable<ArtifactModel>> GetCommunityArtifacts(int id)
        {
            var communityArtifacts = await _artifactDao.GetCommunityArtifacts(id).ConfigureAwait(false);
            return communityArtifacts.Select(_modelFactory.CreateArtifactModel).ToArray();
        }
    }
}
