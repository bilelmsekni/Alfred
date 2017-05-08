using System.Collections.Generic;
using Alfred.Dal.Standard.Entities.Base;

namespace Alfred.Dal.Standard.Entities.Communities
{
    public class CommunityCriteria : BaseCriteria
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> Ids { get; set; }
    }
}
