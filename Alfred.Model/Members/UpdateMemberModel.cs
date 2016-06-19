using Alfred.Dal.Entities.Community;
using Microsoft.Build.Framework;

namespace Alfred.Model.Members
{
    public class UpdateMemberModel
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
    }
}
