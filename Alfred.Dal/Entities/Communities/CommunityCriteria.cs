using System.Collections.Generic;
using Alfred.Dal.Entities.Base;

namespace Alfred.Dal.Entities.Communities
{
    public class CommunityCriteria : BaseCriteria
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> Ids { get; set; }
    }
}
