using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Interfaces;
using Alfred.Model;
using Alfred.Model.Artifacts;

namespace Alfred.Services
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
            var artifactEntity = await _artifactRepo.GetArtifact(id);
            if (artifactEntity != null)
                return _modelFactory.CreateArtifactModel(artifactEntity);
            return null;
        }

        public async Task<int> CreateArtifact(CreateArtifactModel createArtifactModel)
        {
            var artifact = _modelFactory.CreateArtifact(createArtifactModel);
            if (artifact != null)
            {
                return await _artifactRepo.SaveArtifact(artifact);
            }
            return -1;
        }

        public async Task<ArtifactModel> UpdateArtifact(UpdateArtifactModel updateArtifactModel)
        {
            var oldArtifact = await _artifactRepo.GetArtifact(updateArtifactModel.Id);
            if (oldArtifact != null)
            {
                var newArtifact = _modelFactory.CreateArtifact(updateArtifactModel, oldArtifact);
                if (newArtifact != null)
                {
                    _artifactRepo.UpdateArtifact(newArtifact);
                    return _modelFactory.CreateArtifactModel(newArtifact);
                }
            }
            return null;
        }

        public async Task<bool> DeleteArtifact(int id)
        {
            var artifact = await _artifactRepo.GetArtifact(id);
            if (artifact != null)
            {
                await Task.Run(() => _artifactRepo.DeleteArtifact(id));
                return true;
            }
            return false;
        }
    }
}