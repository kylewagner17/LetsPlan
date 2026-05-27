using LetsPlan.Models;
using LetsPlan.Services.Interfaces;
using LetsPlan.ViewModels;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;

namespace LetsPlan.Views;




public partial class SelectedDayPage : ContentPage
{
    private const double PixelsPerMinute = 2.0;

    public SelectedDayPage(DateTime selectedDate, IDatabaseService databaseService)
    {
        InitializeComponent();

        var vm = new SelectedDayViewModel(databaseService);
        BindingContext = vm;

        _ = BuildTimeline(selectedDate);
    }

    private async Task BuildTimeline(DateTime selectedDate)
    {
        if (BindingContext is not SelectedDayViewModel vm)
            return;

        await vm.LoadAsync(selectedDate);

        TimelineCanvas.Children.Clear();

        double leftMargin = 10;

        for (int hour = 6; hour <= 22; hour++)
        {
            var slotTop = (hour - 6) * 120;

            // Hour container
            var slotBorder = new Border
            {
                BackgroundColor = Colors.Transparent,
                Stroke = Color.FromArgb("#E5E7EB"),
                StrokeThickness = 1,
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = 0
                },
                WidthRequest = 1000,
                HeightRequest = 120
            };

            AbsoluteLayout.SetLayoutBounds(
                slotBorder,
                new Rect(0, slotTop, 1000, 120));

            TimelineCanvas.Children.Add(slotBorder);

            // CHECK IF THIS HOUR HAS EVENTS
            bool hasEvent = vm.TimelineEvents.Any(e =>
                e.Event.StartTime.Hour == hour);

            // EMPTY SLOT PLACEHOLDER
            if (!hasEvent)
            {
                var emptyLabel = new Label
                {
                    Text = "None",
                    TextColor = Colors.Gray,
                    FontSize = 12,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                };

                AbsoluteLayout.SetLayoutBounds(
                    emptyLabel,
                    new Rect(110, slotTop + 45, 100, 30));

                TimelineCanvas.Children.Add(emptyLabel);
            }
        }

        foreach (var item in vm.TimelineEvents)
        {
            var evt = item.Event;

            var card = new Border
            {
                BackgroundColor = Color.FromArgb("#4F46E5"),
                StrokeShape = new RoundRectangle { CornerRadius = 12 },
                Padding = 10,
                Content = new VerticalStackLayout
                {
                    Children =
                    {
                        new Label
                        {
                            Text = evt.Title,
                            TextColor = Colors.White,
                            FontAttributes = FontAttributes.Bold
                        },
                        new Label
                        {
                            Text = evt.StartTime.ToString("h:mm tt"),
                            TextColor = Color.FromArgb("#E5E7EB"),
                            FontSize = 12
                        }
                    }
                }
            };

            // POSITIONING (THIS replaces LayoutBounds binding)
            AbsoluteLayout.SetLayoutBounds(
                card,
                new Rect(
                    leftMargin,
                    item.TopOffset,
                    300,
                    item.Height
                )
            );

            AbsoluteLayout.SetLayoutFlags(card, AbsoluteLayoutFlags.None);

            TimelineCanvas.Children.Add(card);
        }

        await Task.Delay(100);

        // Snap to first event
        if (vm.TimelineEvents.Any())
        {
            var first = vm.TimelineEvents.First();
            await MainScroll.ScrollToAsync(0, first.TopOffset, true);
        }
    }
}