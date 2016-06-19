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
        public CommunityRole Role { get; set; }
    }
}
