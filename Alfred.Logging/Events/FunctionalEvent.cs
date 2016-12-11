using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfred.Logging.Events
{
    public class FunctionalEvent : BaseEvent
    {
        public TimeSpan ElapsedTime { get; set; }
        public Dictionary<string, object> ActionArguments { get; set; }
    }
}
