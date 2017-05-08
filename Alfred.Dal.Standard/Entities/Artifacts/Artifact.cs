using Alfred.Shared.Standard.Enums;

namespace Alfred.Dal.Standard.Entities.Artifacts
{
    public class Artifact
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ArtifactType Type { get; set; }
        public ArtifactStatus Status { get; set; }
        public int Bonus { get; set; }
        public int Reward { get; set; }
        public int MemberId { get; set; }
        public int CommunityId { get; set; }
    }
}
