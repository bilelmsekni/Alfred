using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.FakeImplementation.Database;
using Alfred.Dal.FakeImplementation.EntityDtos;

namespace Alfred.Dal.FakeImplementation.Dao
{
    public class ArtifactDao : IArtifactDao
    {
        private readonly List<ArtifactDto> _artifacts = FakeDatabase.ArtifactData;
        
        public IEnumerable<ArtifactDto> GetArtifacts()
        {
            return _artifacts;
        }

        public ArtifactDto GetArtifact(int id)
        {
            return _artifacts.FirstOrDefault(x => x.Id == id);
        }

        public int SaveArtifact(ArtifactDto artifact)
        {
            artifact.Id = _artifacts.Count + 1;
            _artifacts.Add(artifact);
            return artifact.Id;
        }

        public void DeleteArtifact(int id)
        {
            _artifacts.RemoveAt(_artifacts.FindIndex(x=>x.Id == id));
        }

        public ArtifactDto GetArtifact(string title)
        {
            return _artifacts.FirstOrDefault(x => x.Title.ToLowerInvariant() == title.ToLowerInvariant());
        }

        public void UpdateArtifact(ArtifactDto artifact)
        {
            _artifacts.RemoveAt(_artifacts.FindIndex(x => x.Id == artifact.Id));            
            _artifacts.Add(artifact);
        }

        public IEnumerable<ArtifactDto> GetMemberArtifacts(int id)
        {
            return _artifacts.Where(x => x.MemberId == id);
        }

        public IEnumerable<ArtifactDto> GetCommunityArtifacts(int id)
        {
            return _artifacts.Where(x => x.CommunityId == id);
        }
    }
}
