using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlan.Models
{
    public class TimelineEvent
    {
        public Event Event { get; set; }

        public double TopOffset { get; set; }
        public double Height { get; set; }
    }
}
