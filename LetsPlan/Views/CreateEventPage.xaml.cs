using LetsPlan.Models;
using LetsPlan.Services.Interfaces;

namespace LetsPlan.Views;

public partial class CreateEventPage : ContentPage
{
    public Event CreatedEvent = new Event();
    private readonly IDatabaseService _databaseService;

    public CreateEventPage(DateTime selectedDate, IDatabaseService databaseService)
    {
        InitializeComponent();

        DatePicker.Date = selectedDate;

        _databaseService = databaseService;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        CreatedEvent = new Event
        {

            Title = TitleEntry.Text,

            Date = DatePicker.Date,

            StartTime = DatePicker.Date + StartTimePicker.Time,

            EndTime = DatePicker.Date + EndTimePicker.Time,

            LastUpdated = DateTime.Now,

            CreatedByUserId = 1,

            CategoryType = "Test",

            Groups = new List<Group>() 


        };

        await _databaseService.SaveEventAsync(CreatedEvent);
        await DisplayAlert("Alert", "Event object created", "OK");

        await Navigation.PopModalAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}