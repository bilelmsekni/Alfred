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
    }
}