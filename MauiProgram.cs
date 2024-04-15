using Cinemate.Models;
using Cinemate.Views;
using Microsoft.Extensions.Logging;

namespace Cinemate
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Teko-Bold.ttf", "TekoBold");
                    fonts.AddFont("Teko-Light.ttf", "TekoLight");
                    fonts.AddFont("Teko-Medium.ttf", "TekoMedium");
                    fonts.AddFont("Teko-Regular.ttf", "TekoRegular");
                    fonts.AddFont("Teko-SemiBold.ttf", "TekoSemiBold");

                });

            builder.Services.AddSingleton<ImageCollection>();
            builder.Services.AddSingleton<IntroViewModel>();
            builder.Services.AddSingleton<IntroPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
