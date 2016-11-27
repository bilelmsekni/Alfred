using Alfred.Shared.Enums;
using Newtonsoft.Json;

namespace Alfred.Models.Artifacts
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
        public int CommunityId { get; set; }
        public int MemberId { get; set; }
    }
}
