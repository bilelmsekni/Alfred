using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.Entities.Community;
using Alfred.Dal.Entities.Member;
using Microsoft.Build.Framework;

namespace Alfred.Model.Artifacts
{
    public class UpdateArtifactModel
    {
        [Required]
        public string Title { get; set; }        
        public Member Member { get; set; }
        public int Bonus { get; set; }
        public Community Community { get; set; }
        [Required]
        public int Reward { get; set; }
        [Required]
        public ArtifactStatus Status { get; set; }
        [Required]
        public ArtifactType Type { get; set; }
    }
}
