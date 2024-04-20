using Cinemate.Models;
using Cinemate.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Cinemate.ViewModels
{
    public partial class MoviesViewModel : ObservableObject
    {
        readonly MoviesCollection movies;

        [ObservableProperty]
        int entriesCount;

        [ObservableProperty]
        string selectedSource;

        [ObservableProperty]
        MovieLibrary selectedMovie;

        [ObservableProperty]
        private FilterOption selectedFilterOption;

        public ObservableCollection<string> Sources { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> Categories { get; } = new ObservableCollection<string>();
        public List<MovieLibrary> Movies { get; set; } = new List<MovieLibrary>();
        public ObservableCollection<FilterOption> FilterOptions { get; } = new ObservableCollection<FilterOption>();


        public MoviesViewModel(MoviesCollection moviesCollection)
        {
            movies = moviesCollection;
            LoadData();
        }

        DaoMovie daoMovie = DaoMovie.GetDaoMovie();

        void LoadData()
        {
            try
            {
                daoMovie.DeleteAllMovie();
            } catch(Exception e)
            {
                Console.WriteLine($"Error deleting all movies: {e.Message}");
            }
            
            movies.AddToDB();
            Movies = daoMovie.GetMovies().Result;


            Sources.Clear();
            foreach (var source in movies.GetMovieSources())
            {
                Sources.Add(source);
            }
            SelectedSource = "All";

            Categories.Clear();
            foreach (var category in movies.GetMovieCategories())
            {
                Categories.Add(category);
            }

            Movies.Clear();
            foreach (var movie in movies.GetMovies())
            {
                Movies.Add(movie);
            }

            FilterOptions.Clear();
            foreach (var option in movies.GetFilterOptions())
            {
                var filterOption = new FilterOption { Name = option, IsSelected = false };
                FilterOptions.Add(filterOption);
            }

            SelectedFilterOption = FilterOptions.FirstOrDefault();
            ApplyFilters();
        }

        [RelayCommand]
        void SelectFilterOption(FilterOption option)
        {
            if (SelectedFilterOption != null)
            {
                SelectedFilterOption.IsSelected = false;
            }

            SelectedFilterOption = option;

            if (SelectedFilterOption != null)
            {
                SelectedFilterOption.IsSelected = true;
            }

            ApplyFilters();
        }


        [RelayCommand]
        void ApplyFilters()
        {
            var filteredMovies = movies.GetMovies();
            var currentDate = DateTime.Today;

            if (!string.IsNullOrEmpty(SelectedSource) && SelectedSource != "All")
            {
                filteredMovies = filteredMovies.Where(m => m.Status == SelectedSource);
            }

            if (SelectedFilterOption != null)
            {
                switch (SelectedFilterOption.Name)
                {
                    case "A - Z":
                        filteredMovies = filteredMovies.OrderBy(m => m.Title);
                        break;
                    case "Z - A":
                        filteredMovies = filteredMovies.OrderByDescending(m => m.Title);
                        break;
                    case "Score":
                        filteredMovies = filteredMovies.OrderByDescending(m => m.Rating);
                        break;
                    case "Upcoming":
                        filteredMovies = filteredMovies
                            .Where(m => DateTime.ParseExact(m.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture) >= currentDate)
                            .OrderBy(m => m.StartDate);
                        break;
                    case "Past":
                        filteredMovies = filteredMovies
                            .Where(m => DateTime.ParseExact(m.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture) < currentDate)
                            .OrderByDescending(m => m.StartDate);
                        break;
                }
            }

            Movies.Clear();
            foreach (var movie in filteredMovies)
            {
                Movies.Add(movie);
            }

            EntriesCount = Movies.Count;
        }

        partial void OnSelectedSourceChanged(string value)
        {
            ApplyFilters();
        }

        partial void OnSelectedFilterOptionChanged(FilterOption value)
        {
            ApplyFilters();
        }


        [RelayCommand]
        async Task GoToDetails(MovieLibrary movie)
        {
            if (movie == null)
                return;

            await Shell.Current.GoToAsync(nameof(MovieDetailView), true, new Dictionary<string, object>
            {
                {"Movie", movie }
            });
        }

        [RelayCommand]
        async Task NavigateToAddMoviePage()
        {
            await Shell.Current.GoToAsync(nameof(AddMovieToMyList));
        }

    }
}
