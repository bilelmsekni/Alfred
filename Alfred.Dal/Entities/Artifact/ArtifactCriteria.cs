using System.Collections.Generic;
using Alfred.Dal.Entities.Base;
using Alfred.Shared.Enums;

namespace Alfred.Dal.Entities.Artifact
{
    public class ArtifactCriteria : BaseCriteria
    {
        public IEnumerable<int> Ids { get; set; }
        public string Title { get; set; }
        public ArtifactType? Type { get; set; }
        public ArtifactStatus? Status { get; set; }
        public int? CommunityId { get; set; }
        public int? MemberId { get; set; }
    }
}
