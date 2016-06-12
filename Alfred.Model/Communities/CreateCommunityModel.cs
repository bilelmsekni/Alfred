using Microsoft.Build.Framework;

namespace Alfred.Model.Communities
{
    public class CreateCommunityModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
