using System.Collections.Generic;

namespace Alfred.Dal.Entities.Base
{
    public class BaseResponse<T>
    {
        public IEnumerable<T> Results { get; set; }
        public IList<Link> Links { get; set; }
    }
}
