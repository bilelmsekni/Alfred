using System.Collections.Generic;
using Alfred.Models.Base;
using Alfred.Shared.Enums;

namespace Alfred.Models.Members
{
    public class MemberCriteriaModel : BaseCriteriaModel
    {
        public IEnumerable<string> Ids { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public CommunityRole? Role { get; set; }
        public int? CommunityId { get; set; }
    }
}
