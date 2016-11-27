using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfred.Models.Base;

namespace Alfred.Models.Communities
{
    public class CommunityCriteriaModel : BaseCriteriaModel
    {
        public IEnumerable<string> Ids { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
