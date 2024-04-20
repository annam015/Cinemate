using Cinemate.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace Cinemate.ViewModels
{
    [QueryProperty(nameof(MovieLibrary), "MovieLibrary")]
    public partial class MovieDetailViewModel : ObservableObject, INotifyPropertyChanged
    {
        //public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();


        [ObservableProperty]
        MovieLibrary movie;


        [RelayCommand]
        async Task DeleteMovie()
        {
            bool deleteConfirmed = await Shell.Current.DisplayAlert(
                "Delete Movie",
                "Are you sure you want to delete this movie from your journal?",
                "Yes",
                "No"
            );

            if (deleteConfirmed && movie != null)
            {
                DaoMovie daoMovie = DaoMovie.GetDaoMovie();
                await daoMovie.DeleteMovie(movie);
                //if(result > 0)
                //{
                //    Movies.Remove(movie);
                //}


                var remainingMovies = await daoMovie.GetMovies();
                foreach (var remainingMovie in remainingMovies)
                {
                    Console.WriteLine($"Remaining Movie: {remainingMovie.Title}");
                }

                await Shell.Current.GoToAsync("..");
            }
        }


        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
