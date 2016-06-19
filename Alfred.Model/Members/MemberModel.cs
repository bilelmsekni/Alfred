using System.Collections.Generic;
using Alfred.Dal.Entities.Community;
using Alfred.Model.Artifacts;
using Microsoft.Build.Framework;

namespace Alfred.Model.Members
{
    public class MemberModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public CommunityRole Role { get; set; }
        [Required]
        public IEnumerable<ArtifactModel> Artifacts { get; set; }
    }
}
