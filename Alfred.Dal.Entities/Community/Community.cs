using System.Collections.Generic;

namespace Alfred.Dal.Entities.Community
{
    public class Community
    {
        public int Id { get; set; }
        public IEnumerable<Artifact.Artifact> Artifacts { get; set; }
        public IEnumerable<Member.Member> Members { get; set; }
        public string Name { get; set; }        
    }
}