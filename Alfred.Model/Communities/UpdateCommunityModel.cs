using System.Collections.Generic;
using Alfred.Model.Artifacts;
using Alfred.Model.Members;
using Microsoft.Build.Framework;

namespace Alfred.Model.Communities
{
    public class UpdateCommunityModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public IEnumerable<ArtifactModel> Artifacts { get; set; }
        public IEnumerable<MemberModel> Members { get; set; }
    }
}
