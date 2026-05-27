using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsPlan.Models;

namespace LetsPlan.Services.Interfaces
{
    public interface IDatabaseService
    {
        Task<List<Event>> GetEventsAsync();
        Task<List<Event>> GetEventsForDateAsync(DateTime selectedDate);
        Task SaveEventAsync(Event ev);

        Task ClearEventsAsync();
    }
}
