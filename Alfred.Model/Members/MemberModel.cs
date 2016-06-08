using System.Collections.Generic;
using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.Entities.Community;

namespace Alfred.Model.Members
{
    public class MemberModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ComunityRole Role { get; set; }
        public IEnumerable<Community> Communities { get; set; }
        public IEnumerable<Artifact> Artifacts { get; set; }
    }
}
