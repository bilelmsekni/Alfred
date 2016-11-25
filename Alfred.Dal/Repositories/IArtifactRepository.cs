using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Artifact;

namespace Alfred.Dal.Repositories
{
    public interface IArtifactRepository
    {
        Task<IEnumerable<Artifact>> GetArtifacts();
        Task<Artifact> GetArtifact(int id);
        Task<int> SaveArtifact(Artifact artifact);
        Task DeleteArtifact(int id);
        Task UpdateArtifact(Artifact artifact);
        Task<IEnumerable<Artifact>> GetMemberArtifacts(int id);
        Task<IEnumerable<Artifact>> GetCommunityArtifacts(int id);
    }
}
