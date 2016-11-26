using Alfred.Shared.Enums;
using Newtonsoft.Json;

namespace Alfred.Models.Members
{
    public class UpdateMemberModel
    {
        [JsonIgnore]
        public int Id { get; set; }        
        public string Email { get; set; }        
        public string FirstName { get; set; }        
        public string LastName { get; set; }        
        public CommunityRole Role { get; set; }
    }
}
