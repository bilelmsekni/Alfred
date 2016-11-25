using Alfred.Dal.Entities.Enums;
using Newtonsoft.Json;

namespace Alfred.Domain.Models.Members
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
