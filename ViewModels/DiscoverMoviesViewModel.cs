﻿using Cinemate.Models;
using Cinemate.Views.Templates;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace Cinemate.ViewModels
{
    public partial class DiscoverMoviesViewModel : INotifyPropertyChanged
    {
        private Dictionary<string, int> GenreOptions { get; set; }
        private Dictionary<string, string> SortByOptions { get; set; }

        public List<string> ReleaseYears { get; set; }
        public List<string> GenreOptionsKeys { get; set; }
        public List<string> SortOptions { get; set; }

        public string ReleaseYear { get; set; }
        public string Genre { get; set; }
        public string SortBy { get; set; }

        private List<Movie> movies;
        public List<Movie> Movies { get { return movies; } set { movies = value; OnPropertyChanged("Movies"); } }


        public DiscoverMoviesViewModel()
        {
            ReleaseYears = Collections.YearsList;
            GenreOptions = Collections.GenreDictionary;
            GenreOptionsKeys = GenreOptions.Keys.ToList();
            SortByOptions = Collections.SortByDictionary;
            SortOptions = SortByOptions.Keys.ToList();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [RelayCommand]
        private async Task DiscoverMovies()
        {
            if(ReleaseYear == null || Genre == null || SortBy == null)
            {
                await Shell.Current.DisplayAlert("Error", "Please fill in all the fields!", "OK");
                return;
            }
            Movies = await DiscoverMoviesAPI.getMovies(GenreOptions, ReleaseYear, SortByOptions[SortBy], GenreOptions[Genre].ToString());
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