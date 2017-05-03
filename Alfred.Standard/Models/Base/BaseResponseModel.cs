using System.Collections.Generic;

namespace Alfred.Standard.Models.Base
{
    public class BaseResponseModel<T>
    {
        public IEnumerable<T> Results { get; set; }
        public IEnumerable<LinkModel> Links { get; set; }
    }
}
