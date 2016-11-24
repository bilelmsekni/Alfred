using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.Entities.Community;
using Alfred.Dal.Entities.Enums;

namespace Alfred.Dal.Entities.Member
{
    public class Member
    {
        public Member()
        {
            Artifacts = Enumerable.Empty<Artifact.Artifact>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public CommunityRole Role { get; set; }
        public IEnumerable<Artifact.Artifact> Artifacts { get; set; }
    }
}