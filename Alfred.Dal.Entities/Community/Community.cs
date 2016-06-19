using System.Collections.Generic;
using System.Linq;

namespace Alfred.Dal.Entities.Community
{
    public class Community
    {
        public Community()
        {
            Artifacts = Enumerable.Empty<Artifact.Artifact>();
            Members = Enumerable.Empty<Member.Member>();
        }
        public int Id { get; set; }
        public IEnumerable<Artifact.Artifact> Artifacts { get; set; }
        public IEnumerable<Member.Member> Members { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}