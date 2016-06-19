using System.Collections.Generic;
using Alfred.Dal.FakeImplementation.EntityDtos;

namespace Alfred.Dal.FakeImplementation.Dao
{
    public interface IArtifactDao
    {
        IEnumerable<ArtifactDto> GetArtifacts();
        ArtifactDto GetArtifact(int id);
        int SaveArtifact(ArtifactDto artifact);
        void DeleteArtifact(int id);
        ArtifactDto GetArtifact(string title);
        void UpdateArtifact(ArtifactDto artifact);
        IEnumerable<ArtifactDto> GetMemberArtifacts(int id);
        IEnumerable<ArtifactDto> GetCommunityArtifacts(int id);
    }
}