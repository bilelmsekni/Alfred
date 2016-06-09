using System.Collections.Generic;
using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.Entities.Community;
using Microsoft.Build.Framework;

namespace Alfred.Model.Members
{
    public class UpdateMemberModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public ComunityRole Role { get; set; }

        public IEnumerable<Community> Communities { get; set; }
        public IEnumerable<Artifact> Artifacts { get; set; }
    }
}
