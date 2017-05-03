using System.Threading.Tasks;
using Alfred.Standard.Models.Artifacts;

namespace Alfred.Domain.Standard.Repositories
{
    public interface IArtifactRepository
    {
        Task<ArtifactResponseModel> GetArtifacts(ArtifactCriteriaModel criteriaModel);
        Task<ArtifactModel> GetArtifact(int id);
        Task<int> SaveArtifact(CreateArtifactModel artifact);
        Task DeleteArtifact(int id);
        Task<ArtifactModel> UpdateArtifact(UpdateArtifactModel artifact);
    }
}
