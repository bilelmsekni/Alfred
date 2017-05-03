using System.Collections.Generic;
using Alfred.Standard.Models.Base;

namespace Alfred.Standard.Models.Communities
{
    public class CommunityCriteriaModel : BaseCriteriaModel
    {
        public IEnumerable<string> Ids { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
