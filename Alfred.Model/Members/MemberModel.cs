using System.Collections.Generic;
using Alfred.Dal.Entities.Community;
using Alfred.Model.Artifacts;
using Alfred.Model.Communities;

namespace Alfred.Model.Members
{
    public class MemberModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ComunityRole Role { get; set; }
        public IEnumerable<CommunityModel> Communities { get; set; }
        public IEnumerable<ArtifactModel> Artifacts { get; set; }
    }
}
