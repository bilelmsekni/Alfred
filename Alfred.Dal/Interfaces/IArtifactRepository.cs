using System.Collections.Generic;
using Alfred.Dal.Entities.Artifact;

namespace Alfred.Dal.Interfaces
{
    public interface IArtifactRepository
    {
        IEnumerable<Artifact> GetArtifacts();
        Artifact GetArtifact(int id);
        int SaveArtifact(Artifact artifact);
        void DeleteArtifact(int id);
        void UpdateArtifact(Artifact artifact);
        IEnumerable<Artifact> GetMemberArtifacts(int id);
        IEnumerable<Artifact> GetCommunityArtifacts(int id);
    }
}
