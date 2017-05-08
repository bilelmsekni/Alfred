using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Dal.Standard.Daos;
using Alfred.Dal.Standard.Entities.Artifacts;
using Alfred.Dal.Standard.Entities.Base;
using Alfred.Dal.Standard.Mappers;
using Alfred.Standard.Models.Artifacts;
using Alfred.Domain.Standard.Repositories;

namespace Alfred.Dal.Standard.Repositories
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
                Links = CreateLinks(artifactCriteria.Page, artifactCriteria.PageSize, resultCount),
                Results = await CreateResults(artifactCriteria, resultCount).ConfigureAwait(false)
            };

            return _modelFactory.CreateArtifactResponseModel(artifactResponse);
        }

        private async Task<IEnumerable<Artifact>> CreateResults(ArtifactCriteria criteria, int resultCount)
        {
            if (resultCount > 0 && criteria.Page.IsPageInRange(resultCount, criteria.PageSize))
            {
                return (await _artifactDao.GetArtifacts(criteria).ConfigureAwait(false))
                    .Paginate(criteria.Page, criteria.PageSize);
            }
            return new List<Artifact>();
        }

        private IList<Link> CreateLinks(int page, int pageSize, int resultCount)
        {
            return new List<Link>()
                .AddFirstPage(resultCount)
                .AddLastPage(resultCount, pageSize)
                .AddNextPage(resultCount, pageSize, page)
                .AddPreviousPage(resultCount, page);
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

    }
}
