using LetsPlan.Services.Interfaces;
using LetsPlan.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsPlan.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LetsPlan.ViewModels
{

    public partial class SelectedDayViewModel : ObservableObject
    {
        private readonly IDatabaseService _databaseService;

        public ObservableCollection<TimelineEvent> TimelineEvents { get; set; } = new();

        public DateTime SelectedDate { get; set; }

        private const double PixelsPerMinute = 2.0;

        public SelectedDayViewModel(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task LoadAsync(DateTime selectedDate)
        {
            SelectedDate = selectedDate;

            var events = await _databaseService.GetEventsForDateAsync(selectedDate);

            TimelineEvents.Clear();

            var startOfDay = selectedDate.Date;

            foreach (var e in events.OrderBy(x => x.StartTime))
            {
                // 🔥 IMPORTANT FIX: ensure we only use time-of-day portion
                var startMinutes = (e.StartTime - startOfDay).TotalMinutes;
                var durationMinutes = (e.EndTime - e.StartTime).TotalMinutes;

                // fallback safety
                if (durationMinutes <= 0)
                    durationMinutes = 30;

                TimelineEvents.Add(new TimelineEvent
                {
                    Event = e,
                    TopOffset = startMinutes * PixelsPerMinute,
                    Height = durationMinutes * PixelsPerMinute
                });
            }
        }
    }
}
