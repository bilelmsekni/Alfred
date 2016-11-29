using System.Collections.Generic;
using Alfred.Models.Base;
using Alfred.Shared.Enums;

namespace Alfred.Models.Artifacts
{
    public class ArtifactCriteriaModel : BaseCriteriaModel
    {
        public IEnumerable<string> Ids { get; set; }
        public string Title { get; set; }
        public ArtifactType? Type { get; set; }
        public ArtifactStatus? Status { get; set; }
        public int? CommunityId { get; set; }
        public int? MemberId { get; set; }
    }
}
