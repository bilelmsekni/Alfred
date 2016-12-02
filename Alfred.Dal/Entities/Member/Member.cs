using System.Collections.Generic;
using Alfred.Shared.Enums;

namespace Alfred.Dal.Entities.Member
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public CommunityRole Role { get; set; }
        public IList<int> CommunityIds { get; set; }
    }
}