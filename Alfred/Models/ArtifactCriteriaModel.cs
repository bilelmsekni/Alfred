using System.Collections.Generic;
using Alfred.Shared.Enums;

namespace Alfred.Models
{
    public class ArtifactCriteriaModel
    {
        public IEnumerable<string> Ids { get; set; }
        public string Title { get; set; }
        public ArtifactType? Type { get; set; }
        public ArtifactStatus? Status { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
