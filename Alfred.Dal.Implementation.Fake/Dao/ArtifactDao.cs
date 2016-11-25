using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Implementation.Fake.Database;
using Alfred.Dal.Implementation.Fake.EntityDtos;

namespace Alfred.Dal.Implementation.Fake.Dao
{
    public class ArtifactDao : IArtifactDao
    {
        private readonly List<ArtifactDto> _artifacts = FakeDatabase.ArtifactData;

        public async Task<IEnumerable<ArtifactDto>> GetArtifacts()
        {
            return await Task.Run(() => _artifacts).ConfigureAwait(false);
        }

        public async Task<ArtifactDto> GetArtifact(int id)
        {
            return await Task.Run(() => _artifacts.FirstOrDefault(x => x.Id == id)).ConfigureAwait(false);
        }

        public async Task<int> SaveArtifact(ArtifactDto artifact)
        {
            return await Task.Run(() =>
            {
                artifact.Id = _artifacts.Count + 1;
                _artifacts.Add(artifact);
                return artifact.Id;
            }).ConfigureAwait(false);
        }

        public async Task DeleteArtifact(int id)
        {
            await Task.Run(() => _artifacts.RemoveAt(_artifacts.FindIndex(x => x.Id == id))).ConfigureAwait(false);
        }

        public async Task UpdateArtifact(ArtifactDto artifact)
        {
            await Task.Run(() =>
            {
                _artifacts.RemoveAt(_artifacts.FindIndex(x => x.Id == artifact.Id));
                _artifacts.Add(artifact);
            }).ConfigureAwait(false);
        }

        public async Task<IEnumerable<ArtifactDto>> GetMemberArtifacts(int id)
        {
            return await Task.Run(() => _artifacts.Where(x => x.MemberId == id)).ConfigureAwait(false);
        }

        public async Task<IEnumerable<ArtifactDto>> GetCommunityArtifacts(int id)
        {
            return await Task.Run(() => _artifacts.Where(x => x.CommunityId == id)).ConfigureAwait(false);
        }
    }
}
