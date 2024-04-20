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
        //private readonly MoviesCollection movies;
        private readonly MoviesCollection moviesCollection;
        private readonly DaoMovie daoMovie;

        [ObservableProperty]
        private int entriesCount;

        [ObservableProperty]
        private string selectedSource;

        [ObservableProperty]
        private MovieLibrary selectedMovie;

        [ObservableProperty]
        private FilterOption selectedFilterOption;

        public ObservableCollection<string> Sources { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> Categories { get; } = new ObservableCollection<string>();
        //public List<MovieLibrary> Movies { get; set; } = new List<MovieLibrary>();
        public ObservableCollection<MovieLibrary> Movies { get; } = new ObservableCollection<MovieLibrary>();
        public ObservableCollection<FilterOption> FilterOptions { get; } = new ObservableCollection<FilterOption>();


        public MoviesViewModel(MoviesCollection moviesCollection)
        {
            //movies = moviesCollection;
            this.moviesCollection = moviesCollection;
            daoMovie = DaoMovie.GetDaoMovie();
            LoadData();
        }

        //DaoMovie daoMovie = DaoMovie.GetDaoMovie();

        async void LoadData()
        {
            //try
            //{
            //    var moviesFromDb = await daoMovie.GetMovies();

            //    Movies.Clear();
            //    foreach (var movie in moviesFromDb)
            //        Movies.Add(movie);

            //    LoadSourcesAndCategories();
            //    SetupFilterOptions();

            //    SelectedSource = "All";
            //    ApplyFilters();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error loading data: {ex.Message}");
            //}

            //try
            //{
            //    daoMovie.DeleteAllMovie();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine($"Error deleting all movies: {e.Message}");
            //}

            //movies.AddToDB();
            //Movies = daoMovie.GetMovies().Result;

            try
            {
                var moviesFromDb = await daoMovie.GetMovies();
                Movies.Clear();
                foreach (var movie in moviesFromDb)
                    Movies.Add(movie);
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting all movies: {ex.Message}");
            }


            Sources.Clear();
            foreach (var source in moviesCollection.GetMovieSources())
            {
                Sources.Add(source);
            }
            SelectedSource = "All";

            Categories.Clear();
            foreach (var category in moviesCollection.GetMovieCategories())
            {
                Categories.Add(category);
            }

            Movies.Clear();
            foreach (var movie in moviesCollection.GetMovies())
            {
                Movies.Add(movie);
            }

            FilterOptions.Clear();
            foreach (var option in moviesCollection.GetFilterOptions())
            {
                var filterOption = new FilterOption { Name = option, IsSelected = false };
                FilterOptions.Add(filterOption);
            }

            SelectedFilterOption = FilterOptions.FirstOrDefault();
            ApplyFilters();
        }

        //private void LoadSourcesAndCategories()
        //{
        //    Sources.Clear();
        //    foreach (var source in moviesCollection.GetMovieSources())
        //        Sources.Add(source);

        //    Categories.Clear();
        //    foreach (var category in moviesCollection.GetMovieCategories())
        //        Categories.Add(category);
        //}

        //private void SetupFilterOptions()
        //{
        //    FilterOptions.Clear();
        //    foreach (var option in moviesCollection.GetFilterOptions())
        //        FilterOptions.Add(new FilterOption { Name = option, IsSelected = false });

        //    SelectedFilterOption = FilterOptions.FirstOrDefault();
        //}

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
            //var filteredMovies = Movies.ToList();

            //if (!string.IsNullOrEmpty(SelectedSource) && SelectedSource != "All")
            //{
            //    filteredMovies = filteredMovies.Where(m => m.Status == SelectedSource).ToList();
            //}

            //if (SelectedFilterOption != null)
            //{
            //    switch (SelectedFilterOption.Name)
            //    {
            //        case "A - Z":
            //            filteredMovies = filteredMovies.OrderBy(m => m.Title).ToList();
            //            break;
            //        case "Z - A":
            //            filteredMovies = filteredMovies.OrderByDescending(m => m.Title).ToList();
            //            break;
            //        case "Score":
            //            filteredMovies = filteredMovies.OrderByDescending(m => m.Rating).ToList();
            //            break;
            //        case "Upcoming":
            //            filteredMovies = filteredMovies
            //                .Where(m => DateTime.ParseExact(m.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture) >= DateTime.Today)
            //                .OrderBy(m => m.StartDate).ToList();
            //            break;
            //        case "Past":
            //            filteredMovies = filteredMovies
            //                .Where(m => DateTime.ParseExact(m.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture) < DateTime.Today)                                                     
            //                .OrderByDescending(m => m.StartDate).ToList();
            //            break;
            //    }
            //}

            //Movies.Clear();
            //foreach (var movie in filteredMovies)
            //{
            //    Movies.Add(movie);
            //}

            //EntriesCount = Movies.Count;

            var filteredMovies = Movies.ToList();
            var currentDate = DateTime.Today;

            if (!string.IsNullOrEmpty(SelectedSource) && SelectedSource != "All")
            {
                filteredMovies = Movies.Where(m => m.Status == SelectedSource).ToList();
            }

            if (SelectedFilterOption != null)
            {
                switch (SelectedFilterOption.Name)
                {
                    case "A - Z":
                        filteredMovies = Movies.OrderBy(m => m.Title).ToList();
                        break;
                    case "Z - A":
                        filteredMovies = Movies.OrderByDescending(m => m.Title).ToList();
                        break;
                    case "Score":
                        filteredMovies = Movies.OrderByDescending(m => m.Rating).ToList();
                        break;
                    case "Upcoming":
                        filteredMovies = Movies
                            .Where(m => DateTime.ParseExact(m.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture) >= currentDate)
                            .OrderBy(m => m.StartDate).ToList();
                        break;
                    case "Past":
                        filteredMovies = Movies
                            .Where(m => DateTime.ParseExact(m.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture) < currentDate)
                            .OrderByDescending(m => m.StartDate).ToList();
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
                {"MovieLibrary", movie }
            });
        }

        [RelayCommand]
        async Task NavigateToAddMoviePage()
        {
            await Shell.Current.GoToAsync(nameof(AddMovieToMyList));
        }

    }
}
