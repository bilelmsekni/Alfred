using Alfred.Domain.Enums;
using Microsoft.Build.Framework;

namespace Alfred.Model.Members
{
    public class CreateMemberModel
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
