using System.Collections.Generic;
using Alfred.Domain.Entities.Base;

namespace Alfred.Domain.Entities.Community
{
    public class CommunityCriteria : BaseCriteria
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> Ids { get; set; }
    }
}
