using System.Collections.Generic;
using Alfred.Dal.Entities.Community;

namespace Alfred.Dal.Entities.Member
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ComunityRole Role { get; set; }
        public IEnumerable<Artifact.Artifact> Artifacts { get; set; }
    }
}