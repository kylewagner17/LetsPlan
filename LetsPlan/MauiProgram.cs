using LetsPlan.Services;
using LetsPlan.Services.Interfaces;
using LetsPlan.ViewModels;
using LetsPlan.Views;
using Syncfusion.Maui.Core.Hosting;
using Microsoft.Extensions.Logging;

namespace LetsPlan
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            SQLitePCL.Batteries.Init();

            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register Services
            builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
            builder.Services.AddTransient<CalendarViewModel>();
            builder.Services.AddTransient<CalendarPage>();

            return builder.Build();
        }
    }
}
