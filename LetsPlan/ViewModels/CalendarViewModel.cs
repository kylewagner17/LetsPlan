using LetsPlan.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsPlan.Models;

namespace LetsPlan.ViewModels
{
    public class CalendarViewModel
    {
        private readonly IDatabaseService _databaseService;

        public ObservableCollection<Event> Events { get; set; } = new();

        public CalendarViewModel(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task LoadEventsAsync()
        {
            Events.Clear();

            var events = await _databaseService.GetEventsAsync();

            foreach (var ev in events)
                Events.Add(ev);
        }

        public async Task SaveEvent(Event newEvent)
        {
            await _databaseService.SaveEventAsync(newEvent);
        }

        public async Task AddSampleEvent()
        {
            var newEvent = new Event
            {
                Title = "Test Event",
                LastUpdated = DateTime.Now
            };

            await _databaseService.SaveEventAsync(newEvent);
            await LoadEventsAsync();
        }

        public async Task ClearEvents()
        {
            await _databaseService.ClearEventsAsync();
        }


        
    }
}
