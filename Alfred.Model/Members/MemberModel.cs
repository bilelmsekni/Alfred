using System.Collections.Generic;
using Alfred.Dal.Entities.Community;
using Alfred.Model.Artifacts;
using Alfred.Model.Communities;
using Microsoft.Build.Framework;

namespace Alfred.Model.Members
{
    public class MemberModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public ComunityRole Role { get; set; }        
        public IEnumerable<ArtifactModel> Artifacts { get; set; }
    }
}
