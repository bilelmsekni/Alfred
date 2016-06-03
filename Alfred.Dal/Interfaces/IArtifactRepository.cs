using System.Collections.Generic;
using Alfred.Dal.Entities;

namespace Alfred.Dal.Interfaces
{
    public interface IArtifactRepository
    {
        ICollection<Artifact> GetArtifacts();
        Artifact GetArtifact(int id);
        void SaveArtifact(Artifact artifact);
        void DeleteArtifact(int id);
    }
}
