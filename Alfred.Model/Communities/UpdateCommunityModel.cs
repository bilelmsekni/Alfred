using Microsoft.Build.Framework;
using Newtonsoft.Json;

namespace Alfred.Model.Communities
{
    public class UpdateCommunityModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
