using System.Collections.Generic;
using Alfred.Dal.Entities.Artifact;

namespace Alfred.Dal.Interfaces
{
    public interface IArtifactRepository
    {
        IEnumerable<Artifact> GetArtifacts();
        Artifact GetArtifact(int id);
        void SaveArtifact(Artifact artifact);
        void DeleteArtifact(int id);
    }
}
