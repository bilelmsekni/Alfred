using System.Collections.Generic;
using Alfred.Model.Artifacts;
using Alfred.Model.Members;

namespace Alfred.Model.Communities
{
    public class UpdateCommunityModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<ArtifactModel> Artifacts { get; set; }
        public IEnumerable<MemberModel> Members { get; set; }
    }
}
