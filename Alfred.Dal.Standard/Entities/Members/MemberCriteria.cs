using System.Collections.Generic;
using Alfred.Dal.Standard.Entities.Base;
using Alfred.Shared.Standard.Enums;

namespace Alfred.Dal.Standard.Entities.Members
{
    public class MemberCriteria : BaseCriteria
    {
        public IEnumerable<int> Ids { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public CommunityRole? Role { get; set; }
        public int? CommunityId { get; set; }
    }
}
