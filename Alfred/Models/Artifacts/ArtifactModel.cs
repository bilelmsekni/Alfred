using Alfred.Shared.Enums;
using Microsoft.Build.Framework;

namespace Alfred.Models.Artifacts
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
        [Required]
        public int CommunityId { get; set; }
        public int MemberId { get; set; }
        public int Bonus { get; set; }
    }
}
