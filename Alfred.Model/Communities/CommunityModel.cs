using System.Collections.Generic;
using Alfred.Model.Artifacts;
using Alfred.Model.Members;
using Microsoft.Build.Framework;

namespace Alfred.Model.Communities
{
    public class CommunityModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }        
        [Required]
        public string Email { get; set; }
        [Required]
        public IEnumerable<MemberModel> Members { get; set; }
        [Required]
        public IEnumerable<ArtifactModel> Artifacts { get; set; }
    }
}
