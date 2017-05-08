using System.Collections.Generic;
using Alfred.Dal.Standard.Entities.Base;
using Alfred.Shared.Standard.Enums;

namespace Alfred.Dal.Standard.Entities.Artifacts
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
