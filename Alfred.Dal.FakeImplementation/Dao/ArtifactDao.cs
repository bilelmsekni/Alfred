using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.FakeImplementation.EntityDtos;

namespace Alfred.Dal.FakeImplementation.Dao
{
    public class ArtifactDao : IArtifactDao
    {
        private readonly IEnumerable<ArtifactDto> _artifacts;

        public ArtifactDao()
        {
            _artifacts = new List<ArtifactDto>
            {
                new ArtifactDto
                {
                    Title = "Clean Code Support",
                    Id = 1,
                    Bonus = 0,
                    Status = 1,
                    Type = 0,
                    Reward = 200,
                    MemberId = 0,
                    CommunityId = 1
                },
                new ArtifactDto
                {
                    Title = "ElasticSearch presentation",
                    Id = 2,
                    Bonus = 0,
                    Status = 1,
                    Type = 2,
                    Reward = 100,
                    MemberId = 1,
                    CommunityId = 0
                
                },
                new ArtifactDto
                {
                    Title = "Formation BDD",
                    Id = 3,
                    Bonus = 0,
                    Status = 2,
                    Type = 0,
                    Reward = 370,
                    MemberId = -1,
                    CommunityId = 0
                },
                new ArtifactDto
                {
                    Title = "Formation Agile",
                    Id = 3,
                    Bonus = 0,
                    Status = 2,
                    Type = 1,
                    Reward = 520,
                    MemberId = 2,
                    CommunityId = 0
                }
            };
        }
        public IEnumerable<ArtifactDto> GetArtifacts()
        {
            return _artifacts;
        }

        public ArtifactDto GetArtifact(int id)
        {
            return _artifacts.FirstOrDefault(x => x.Id == id);
        }

        public void SaveArtifact(ArtifactDto artifact)
        {
            artifact.Id = _artifacts.Count() + 1;
            _artifacts.ToList().Add(artifact);
        }

        public void DeleteArtifact(int id)
        {
            _artifacts.ToList().RemoveAt(_artifacts.ToList().FindIndex(x=>x.Id == id));
        }

        public ArtifactDto GetArtifact(string title)
        {
            return _artifacts.ToList().FirstOrDefault(x => x.Title.ToLowerInvariant() == title.ToLowerInvariant());
        }

        public void UpdateArtifact(ArtifactDto artifact)
        {
            _artifacts.ToList().RemoveAt(_artifacts.ToList().FindIndex(x => x.Id == artifact.Id));            
            _artifacts.ToList().Add(artifact);
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
