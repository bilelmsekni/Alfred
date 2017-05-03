using System.Collections.Generic;
using Alfred.Shared.Standard.Enums;
using Alfred.Standard.Models.Base;

namespace Alfred.Standard.Models.Members
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
