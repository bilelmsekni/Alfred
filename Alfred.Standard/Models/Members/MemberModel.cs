using System;
using System.Collections.Generic;
using Alfred.Shared.Standard.Enums;
using Alfred.Standard.Models.Communities;

namespace Alfred.Standard.Models.Members
{
    public class MemberModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CommunityRole Role { get; set; }
        public IEnumerable<CommunityModel> Communities { get; set; }
        public DateTime CreationDate { get; set; }
        public string Job { get; set; }
        public string ImageUrl { get; set; }
        public int Gender { get; set; }
    }
}
