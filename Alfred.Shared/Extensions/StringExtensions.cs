using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfred.Shared.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SafeSplit(this string ids, char splitter = ';')
        {
            return ids.Split(new[] { splitter }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
        }
    }
}
