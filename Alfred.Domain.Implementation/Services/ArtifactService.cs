using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Repositories;
using Alfred.Domain.Mappers;
using Alfred.Domain.Models.Artifacts;
using Alfred.Domain.Services;

namespace Alfred.Domain.Implementation.Services
{
    public class ArtifactService : IArtifactService
    {
        private readonly IModelFactory _modelFactory;
        private readonly IArtifactRepository _artifactRepo;

        public ArtifactService(IArtifactRepository artifactRepo, IModelFactory modelFactory)
        {
            _artifactRepo = artifactRepo;
            _modelFactory = modelFactory;
        }

        public async Task<IEnumerable<ArtifactModel>> GetArtifacts()
        {
            var artifactEntities = await _artifactRepo.GetArtifacts().ConfigureAwait(false);
            return artifactEntities.Select(x => _modelFactory.CreateArtifactModel(x));
        }

        public async Task<ArtifactModel> GetArtifact(int id)
        {
            var artifactEntity = await _artifactRepo.GetArtifact(id).ConfigureAwait(false);
            if (artifactEntity != null)
                return _modelFactory.CreateArtifactModel(artifactEntity);
            return null;
        }

        public async Task<int> CreateArtifact(CreateArtifactModel createArtifactModel)
        {
            var artifact = _modelFactory.CreateArtifact(createArtifactModel);
            if (artifact != null)
            {
                return await _artifactRepo.SaveArtifact(artifact).ConfigureAwait(false);
            }
            return -1;
        }

        public async Task<ArtifactModel> UpdateArtifact(UpdateArtifactModel updateArtifactModel)
        {
            var oldArtifact = await _artifactRepo.GetArtifact(updateArtifactModel.Id).ConfigureAwait(false);
            if (oldArtifact != null)
            {
                var newArtifact = _modelFactory.CreateArtifact(updateArtifactModel, oldArtifact);
                if (newArtifact != null)
                {
                    await _artifactRepo.UpdateArtifact(newArtifact).ConfigureAwait(false);
                    return _modelFactory.CreateArtifactModel(newArtifact);
                }
            }
            return null;
        }

        public async Task<bool> DeleteArtifact(int id)
        {
            var artifact = await _artifactRepo.GetArtifact(id).ConfigureAwait(false);
            if (artifact != null)
            {
                await _artifactRepo.DeleteArtifact(id).ConfigureAwait(false);
                return true;
            }
            return false;
        }
    }
}