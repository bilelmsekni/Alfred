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

        public ArtifactModel CreateArtifact(CreateArtifactModel createArtifactModel)
        {
            var artifact = _modelFactory.CreateArtifact(createArtifactModel);
            if (artifact != null && !IsTitleUsed(artifact.Title))
            {
                _artifactRepo.SaveArtifact(artifact);
                return _modelFactory.CreateArtifactModel(artifact);
            }
            return null;
        }

        public ArtifactModel UpdateArtifact(UpdateArtifactModel updateArtifactModel)
        {
            var artifact = _modelFactory.CreateArtifact(updateArtifactModel);
            if (artifact != null && IsTitleUsed(artifact.Title))
            {
                _artifactRepo.UpdateArtifact(artifact);
                return _modelFactory.CreateArtifactModel(artifact);
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

        private bool IsTitleUsed(string title)
        {
            return _artifactRepo.GetArtifact(title) != null;
        }
    }
}