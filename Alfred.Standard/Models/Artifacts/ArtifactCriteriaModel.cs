using System.Collections.Generic;
using Alfred.Shared.Standard.Enums;
using Alfred.Standard.Models.Base;

namespace Alfred.Standard.Models.Artifacts
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
