using Cinemate.Models;
using Cinemate.Views;
using Cinemate.Services;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Cinemate.ViewModels;

namespace Cinemate
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Teko-Bold.ttf", "TekoBold");
                });

            builder.Services.AddSingleton<Collections>();
            builder.Services.AddSingleton<IntroViewModel>();
            builder.Services.AddSingleton<IntroPage>();

            builder.Services.AddTransient<MoviesViewModel>();
            builder.Services.AddTransient<MoviesView>();

            builder.Services.AddTransient<AddMovieToMyListViewModel>();
            builder.Services.AddTransient<AddMovieToMyList>();
            builder.Services.AddTransient<MovieDetailViewModel>();
            builder.Services.AddTransient<MovieDetailView>();

            FormHandler.RemoveBorders();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}