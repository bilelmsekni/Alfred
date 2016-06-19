using Alfred.Dal.Entities.Artifact;
using Newtonsoft.Json;

namespace Alfred.Model.Artifacts
{
    public class UpdateArtifactModel
    {
        [JsonIgnore]        
        public int Id { get; set; }
        public string Title { get; set; }        
        public int Bonus { get; set; }
        public int Reward { get; set; }
        public ArtifactStatus Status { get; set; }
        public ArtifactType Type { get; set; }
    }
}
