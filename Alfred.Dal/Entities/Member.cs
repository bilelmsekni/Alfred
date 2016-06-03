using System.Collections.Generic;

namespace Alfred.Dal.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ComunityRole Role { get; set; }
        public List<Artifact> Artifacts { get; set; }
        public List<Community> Communities { get; set; }
    }
}