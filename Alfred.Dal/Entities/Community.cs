using System.Collections.Generic;

namespace Alfred.Dal.Entities
{
    public class Community
    {
        public int Id { get; set; }
        public List<Artifact> Artifacts { get; set; }
        public List<Member> Members { get; set; }
        public string Name { get; set; }        
    }
}