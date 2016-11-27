using System.Collections.Generic;
using Alfred.Shared.Enums;

namespace Alfred.Domain.Entities.Criteria
{
    public class ArtifactCriteria
    {
        public IEnumerable<int> Ids { get; set; }
        public string Title { get; set; }
        public ArtifactType? Type { get; set; }
        public ArtifactStatus? Status { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
