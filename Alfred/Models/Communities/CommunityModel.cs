using Microsoft.Build.Framework;

namespace Alfred.Models.Communities
{
    public class CommunityModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
