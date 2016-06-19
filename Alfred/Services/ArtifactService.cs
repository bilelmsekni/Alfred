using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<ArtifactModel> GetArtifacts()
        {
            var artifactEntities = _artifactRepo.GetArtifacts();
            return artifactEntities.Select(x => _modelFactory.CreateArtifactModel(x));
        }

        public ArtifactModel GetArtifact(int id)
        {
            var artifactEntity = _artifactRepo.GetArtifact(id);
            if (artifactEntity != null)
                return _modelFactory.CreateArtifactModel(artifactEntity);
            return null;
        }

        public int CreateArtifact(CreateArtifactModel createArtifactModel)
        {
            var artifact = _modelFactory.CreateArtifact(createArtifactModel);
            if (artifact != null)
            {
                return _artifactRepo.SaveArtifact(artifact);
            }
            return -1;
        }

        public ArtifactModel UpdateArtifact(UpdateArtifactModel updateArtifactModel)
        {
            var oldArtifact = _artifactRepo.GetArtifact(updateArtifactModel.Id);
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

        public bool DeleteArtifact(int id)
        {
            var artifact = _artifactRepo.GetArtifact(id);
            if (artifact != null)
            {
                _artifactRepo.DeleteArtifact(id);
                return true;
            }
            return false;
        }
    }
}