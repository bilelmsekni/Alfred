using System.Collections.Generic;
using Alfred.Domain.Entities.Base;
using Alfred.Shared.Enums;

namespace Alfred.Domain.Entities.Member
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
