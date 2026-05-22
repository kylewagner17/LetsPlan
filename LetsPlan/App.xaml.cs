using LetsPlan.Views;

namespace LetsPlan
{
    public partial class App : Application
    {
        private readonly CalendarPage _calendarPage;

        public App(CalendarPage calendarPage)
        {
            InitializeComponent();
            _calendarPage = calendarPage;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(_calendarPage);
        }
    }
}