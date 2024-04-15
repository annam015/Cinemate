using Cinemate.Services;
using System.ComponentModel;

namespace Cinemate.ViewModels
{
    public class MovieSuggestionsViewModel : INotifyPropertyChanged
    {
        private Dictionary<string, int> GenreOptions { get; set; }
        public List<string> GenreOptionsKeys { get; set; }

        public List<string> NumberOptions { get; set; }

        public string NoSuggestions { get; set; }
        public string FavMovies { get; set; }
        public string Genre { get; set; }
        public string NoYearsPublished { get; set; }

        public Command GetSuggestionsCommand { get; set; }

        private string suggestions;
        public string Suggestions { get { return suggestions; } set { suggestions = value; OnPropertyChanged("Suggestions"); } }


        public MovieSuggestionsViewModel()
        {
            GetSuggestionsCommand = new Command(async () => await GetSuggestions());
            GenreOptions = MovieGenreService.GetGenres();
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            NumberOptions = Enumerable.Range(1, 15).Select(y => y.ToString()).ToList();
            GenreOptionsKeys = GenreOptions.Keys.ToList();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task GetSuggestions()
        {
            Suggestions = await MovieSuggestionsAPI.GetSuggestions(int.Parse(NoSuggestions), FavMovies, Genre, int.Parse(NoYearsPublished));
            await Shell.Current.DisplayAlert("Movie Suggestions", Suggestions, "OK");
        }
    }
}
