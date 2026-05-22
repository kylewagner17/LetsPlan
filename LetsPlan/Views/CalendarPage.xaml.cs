// Views/CalendarPage.xaml.cs
using LetsPlan.Services.Interfaces;
using LetsPlan.ViewModels;
using Syncfusion.Maui.Calendar;

namespace LetsPlan.Views;

public partial class CalendarPage : ContentPage
{
    private CalendarViewModel _viewModel;
    private readonly IDatabaseService _databaseService;

    public CalendarPage(CalendarViewModel viewModel, IDatabaseService databaseService)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        _databaseService = databaseService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadEventsAsync();
    }

    private async void OnClearEventClicked(object sender, EventArgs e)
    {
        await _viewModel.ClearEvents();
    }

    public async void OnLongPress(object sender, CalendarLongPressedEventArgs e)
    {
        if (e.Element != CalendarElement.CalendarCell)
        {
            return;
        }

        var modalPage = new CreateEventPage(e.Date, _databaseService);

        await Navigation.PushModalAsync(modalPage);


        if (modalPage.CreatedEvent != null)
        {
            
            await _viewModel.SaveEvent(modalPage.CreatedEvent);
        }

    }
}
