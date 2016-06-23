using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Artifact;

namespace Alfred.Dal.Interfaces
{
    public interface IArtifactRepository
    {
        Task<IEnumerable<Artifact>> GetArtifacts();
        Task<Artifact> GetArtifact(int id);
        Task<int> SaveArtifact(Artifact artifact);
        void DeleteArtifact(int id);
        void UpdateArtifact(Artifact artifact);
        IEnumerable<Artifact> GetMemberArtifacts(int id);
        IEnumerable<Artifact> GetCommunityArtifacts(int id);
    }
}
