using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPlan.Models
{
    public class EventGroup : ObservableCollection<Event>
    {
        public string GroupTitle { get; set; }

        public EventGroup(string title, IEnumerable<Event> events) : base(events) 
        {        
            GroupTitle = title;

        }
    }
}
