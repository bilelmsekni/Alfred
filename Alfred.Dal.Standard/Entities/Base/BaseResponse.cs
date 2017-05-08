using System.Collections.Generic;

namespace Alfred.Dal.Standard.Entities.Base
{
    public class BaseResponse<T>
    {
        public IEnumerable<T> Results { get; set; }
        public IList<Link> Links { get; set; }
    }
}
