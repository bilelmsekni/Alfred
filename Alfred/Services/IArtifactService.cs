using System.Threading.Tasks;
using Alfred.Models.Artifacts;

namespace Alfred.Services
{
    public interface IArtifactService
    {
        Task<ArtifactResponseModel> GetArtifacts(ArtifactCriteriaModel criteriaModel);
        Task<ArtifactModel> GetArtifact(int id);
        Task<int> CreateArtifact(CreateArtifactModel createArtifactModel);
        Task<ArtifactModel> UpdateArtifact(UpdateArtifactModel updateArtifactModel);
        Task<bool> DeleteArtifact(int id);
    }
}
