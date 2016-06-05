using System.Collections.Generic;

namespace Alfred.Dal.Entities
{
    public class Community
    {
        public int Id { get; set; }
        public IEnumerable<Artifact> Artifacts { get; set; }
        public IEnumerable<Member> Members { get; set; }
        public string Name { get; set; }        
    }
}