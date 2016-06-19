using Alfred.Dal.Entities.Artifact;
using Microsoft.Build.Framework;

namespace Alfred.Model.Artifacts
{
    public class ArtifactModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Reward { get; set; }
        [Required]
        public ArtifactType Type { get; set; }
        [Required]
        public ArtifactStatus Status { get; set; }
        public int Bonus { get; set; }
    }
}
