using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Models.Artifacts;

namespace Alfred.Domain.Repositories
{
    public interface IArtifactRepository
    {
        Task<ArtifactResponseModel> GetArtifacts(ArtifactCriteriaModel criteriaModel);
        Task<ArtifactResponseModel> GetArtifact(int id);
        Task<int> SaveArtifact(CreateArtifactModel artifact);
        Task DeleteArtifact(int id);
        Task<ArtifactModel> UpdateArtifact(UpdateArtifactModel artifact);
        Task<IEnumerable<ArtifactModel>> GetMemberArtifacts(int id);
        Task<IEnumerable<ArtifactModel>> GetCommunityArtifacts(int id);
    }
}
