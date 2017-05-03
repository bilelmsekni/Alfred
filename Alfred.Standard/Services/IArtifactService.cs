using System.Threading.Tasks;
using Alfred.Standard.Models.Artifacts;

namespace Alfred.Standard.Services
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
