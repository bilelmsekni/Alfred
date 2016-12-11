using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfred.Logging.Events
{
    public class TechnicalEvent : BaseEvent
    {
        public Exception EventException { get; set; }
        public Dictionary<string, object> ActionArguments { get; set; }
        public Uri RequestUrl { get; set; }
    }
}
