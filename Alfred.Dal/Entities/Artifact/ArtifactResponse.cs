using System.Collections.Generic;
using Alfred.Dal.Entities.Base;

namespace Alfred.Dal.Entities.Artifact
{
    public class ArtifactResponse
    {
        public ArtifactResponse()
        {
            Links = new List<Link>();
            Artifacts= new List<Artifact>();
        }
        public IEnumerable<Artifact> Artifacts { get; set; }
        public IList<Link> Links { get; set; }
    }
}
