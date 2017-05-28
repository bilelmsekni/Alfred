using System.ComponentModel.DataAnnotations;
using Alfred.Shared.Standard.Enums;

namespace Alfred.Standard.Models.Members
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
