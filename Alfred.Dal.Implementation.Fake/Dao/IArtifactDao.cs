using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Dal.Implementation.Fake.EntityDtos;

namespace Alfred.Dal.Implementation.Fake.Dao
{
    public interface IArtifactDao
    {
        Task<IEnumerable<ArtifactDto>> GetArtifacts();
        Task<ArtifactDto> GetArtifact(int id);
        Task<int> SaveArtifact(ArtifactDto artifact);
        Task DeleteArtifact(int id);
        Task UpdateArtifact(ArtifactDto artifact);
        Task<IEnumerable<ArtifactDto>> GetMemberArtifacts(int id);
        Task<IEnumerable<ArtifactDto>> GetCommunityArtifacts(int id);
    }
}