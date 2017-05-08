using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Dal.Standard.Entities.Artifacts;

namespace Alfred.Dal.Standard.Daos
{
    public interface IArtifactDao
    {
        Task<int> CountArtifact(ArtifactCriteria artifactCriteria);
        Task<IEnumerable<Artifact>> GetArtifacts(ArtifactCriteria artifactCriteria);
        Task<Artifact> GetArtifact(int id);
        Task<int> SaveArtifact(Artifact artifact);
        Task DeleteArtifact(int id);
        Task UpdateArtifact(Artifact artifact);
    }
}