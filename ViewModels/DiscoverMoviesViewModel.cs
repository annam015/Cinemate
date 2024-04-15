using Cinemate.Models;
using Cinemate.Views.Templates;
using System.ComponentModel;

namespace Cinemate.ViewModels
{
    public class DiscoverMoviesViewModel : INotifyPropertyChanged
    {
        public List<string> ReleaseYears { get; set; }

        private Dictionary<string, int> GenreOptions { get; set; }
        public List<string> GenreOptionsKeys { get; set; }

        private Dictionary<string, string> SortByOptions { get; set; }
        public List<string> SortOptions { get; set; }


        public string ReleaseYear { get; set; }
        public string Genre { get; set; }
        public string SortBy { get; set; }

        public Command GetMoviesCommand { get; set; }


        private List<Movie> movies;
        public List<Movie> Movies { get { return movies; } set { movies = value; OnPropertyChanged("Movies"); } }


        public DiscoverMoviesViewModel()
        {
            GetMoviesCommand = new Command(async () => await DiscoverMovies());
            SortByOptions = new Dictionary<string, string>();
            SortByOptions.Add("Popularity", "popularity.desc");
            SortByOptions.Add("Release date (ascending)", "primary_release_date.asc");
            SortByOptions.Add("Release date (descending)", "primary_release_date.desc");
            SortByOptions.Add("Number of votes", "vote_count.desc");
            GenreOptions = MovieGenreService.GetGenres();
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            ReleaseYears = Enumerable.Range(1885, DateTime.Now.Year - 1885 + 1).Select(y => y.ToString()).ToList();
            SortOptions = SortByOptions.Keys.ToList();
            GenreOptionsKeys = GenreOptions.Keys.ToList();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task DiscoverMovies()
        {
            string releaseYear = ReleaseYear;
            string sortBy = SortBy != null ? SortByOptions[SortBy] : null;
            string genreID = Genre != null ? GenreOptions[Genre].ToString() : null;
            Movies = await DiscoverMoviesAPI.getMovies(GenreOptions, releaseYear, sortBy, genreID);
            if (Movies != null && Movies.Any())
            {
                await Shell.Current.Navigation.PushAsync(new DiscoveredMoviesTemplate(Movies));
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Could not retrieve movies. Please try again!", "OK");
            }
        }
    }
}
