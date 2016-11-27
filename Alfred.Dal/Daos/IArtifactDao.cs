using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Domain.Entities.Artifact;
using Alfred.Domain.Entities.Criteria;

namespace Alfred.Dal.Daos
{
    public interface IArtifactDao
    {
        Task<IEnumerable<Artifact>> GetArtifacts(ArtifactCriteria artifactCriteria);
        Task<Artifact> GetArtifact(int id);
        Task<int> SaveArtifact(Artifact artifact);
        Task DeleteArtifact(int id);
        Task UpdateArtifact(Artifact artifact);
        Task<IEnumerable<Artifact>> GetMemberArtifacts(int id);
        Task<IEnumerable<Artifact>> GetCommunityArtifacts(int id);
    }
}