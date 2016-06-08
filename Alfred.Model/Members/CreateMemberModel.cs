using Alfred.Dal.Entities.Community;
using Microsoft.Build.Framework;

namespace Alfred.Model.Members
{
    public class CreateMemberModel
    {
        [Required]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public ComunityRole Role { get; set; }
    }
}
