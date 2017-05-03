using Alfred.Shared.Standard.Enums;
using Microsoft.Build.Framework;

namespace Alfred.Standard.Models.Artifacts
{
    public class CreateArtifactModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public ArtifactType Type { get; set; }
        [Required]
        public int Reward { get; set; }
        [Required]
        public int CommunityId { get; set; }
        public int Bonus { get; set; }
        public int MemberId { get; set; }
    }
}
