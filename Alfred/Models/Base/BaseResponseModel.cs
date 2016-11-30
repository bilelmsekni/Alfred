using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfred.Models.Base
{
    public class BaseResponseModel<T>
    {
        public IEnumerable<T> Results { get; set; }
        public IEnumerable<LinkModel> Links { get; set; }
    }
}
