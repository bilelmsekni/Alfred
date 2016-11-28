using System.Threading.Tasks;
using Alfred.Domain.Repositories;
using Alfred.Models.Artifacts;
using Alfred.Services;

namespace Alfred.Domain.Services
{
    public class ArtifactService : IArtifactService
    {
        private readonly IArtifactRepository _artifactRepo;

        public ArtifactService(IArtifactRepository artifactRepo)
        {
            _artifactRepo = artifactRepo;
        }

        public async Task<ArtifactResponseModel> GetArtifacts(ArtifactCriteriaModel criteriaModel)
        {
            return await _artifactRepo.GetArtifacts(criteriaModel).ConfigureAwait(false);
        }

        public async Task<ArtifactModel> GetArtifact(int id)
        {
            return await _artifactRepo.GetArtifact(id).ConfigureAwait(false);
        }

        public async Task<int> CreateArtifact(CreateArtifactModel createArtifactModel)
        {
            if (createArtifactModel != null)
            {
                return await _artifactRepo.SaveArtifact(createArtifactModel).ConfigureAwait(false);
            }
            return -1;
        }

        public async Task<ArtifactModel> UpdateArtifact(UpdateArtifactModel updateArtifactModel)
        {
            if (updateArtifactModel != null)
            {
                await _artifactRepo.UpdateArtifact(updateArtifactModel).ConfigureAwait(false);
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