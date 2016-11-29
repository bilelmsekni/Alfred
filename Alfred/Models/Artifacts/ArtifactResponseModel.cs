using System.Collections.Generic;
using Alfred.Models.Base;

namespace Alfred.Models.Artifacts
{
    public class ArtifactResponseModel
    {
        public IEnumerable<ArtifactModel> Artifacts { get; set; }
        public IEnumerable<LinkModel> Links { get; set; }
    }
}
