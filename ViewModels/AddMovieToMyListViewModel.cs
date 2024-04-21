using Cinemate.Models;
using Cinemate.Models.Cinemate.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace Cinemate.ViewModels
{
    public partial class AddMovieToMyListViewModel : ObservableObject, INotifyPropertyChanged
    {
        //private readonly MovieLibrary movieLibrary;
        DaoMovie daoMovie = DaoMovie.GetDaoMovie();

        private Stream stream;

        [ObservableProperty]
        private bool isActionSelected;
        [ObservableProperty]
        private bool isAdventureSelected;
        [ObservableProperty]
        private bool isAnimationSelected;
        [ObservableProperty]
        private bool isComedySelected;
        [ObservableProperty]
        private bool isCrimeSelected;
        [ObservableProperty]
        private bool isDramaSelected;
        [ObservableProperty]
        private bool isFamilySelected;
        [ObservableProperty]
        private bool isHistorySelected;
        [ObservableProperty]
        private bool isHorrorSelected;
        [ObservableProperty]
        private bool isMusicSelected;
        [ObservableProperty]
        private bool isMysterySelected;
        [ObservableProperty]
        private bool isRomanceSelected;


        public List<string> MovieOptions { get; set; }

        public AddMovieToMyListViewModel()
        {
            MovieOptions = PickerOptions.MovieStatuses;
        }


        [ObservableProperty]
        private ImageSource selectedImageSource;

        [RelayCommand]
        private async Task PickImage()
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Pick Image",
                FileTypes = FilePickerFileType.Png
            });

            if (result == null)
                return;

            //var stream = await result.OpenReadAsync();
            stream = await result.OpenReadAsync();
            SelectedImageSource = ImageSource.FromStream(() => stream);
        }


        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private string summary;

        [ObservableProperty]
        private string myReview;

        [ObservableProperty]
        private DateTime startDate = DateTime.Today;

        [ObservableProperty]
        private double rating;

        [ObservableProperty]
        private string selectedStatus;
        // imaginea
        //[ObservableProperty]
        //private string selectedImage;



        public List<string> GetSelectedCategories()
        {
            var selectedCategories = new List<string>();
            if (IsActionSelected) selectedCategories.Add("Action");
            if (IsAdventureSelected) selectedCategories.Add("Adventure");
            if (IsAnimationSelected) selectedCategories.Add("Animation");
            if (IsComedySelected) selectedCategories.Add("Comedy");
            if (IsCrimeSelected) selectedCategories.Add("Crime");
            if (IsDramaSelected) selectedCategories.Add("Drama");
            if (IsFamilySelected) selectedCategories.Add("Family");
            if (IsHistorySelected) selectedCategories.Add("History");
            if (IsHorrorSelected) selectedCategories.Add("Horror");
            if (IsMusicSelected) selectedCategories.Add("Music");
            if (IsMysterySelected) selectedCategories.Add("Mystery");
            if (IsRomanceSelected) selectedCategories.Add("Romance");

            return selectedCategories;
        }


        [RelayCommand]
        public async Task AddMovie()
        {
            var selectedCategories = GetSelectedCategories();
            var random = new Random();

            var newMovie = new MovieLibrary
            {
                Title = this.Title,
                Rating = this.Rating,
                StartDate = this.StartDate.ToString("dd-MM-yyyy"),
                Summary = this.Summary,
                MyReview = this.MyReview,
                Status = this.SelectedStatus,
                Categories = selectedCategories,
                Cover = ImageConverter.ImageToBase64(stream)
            };

            newMovie.Reviews = random.Next(1000, 5000);
            newMovie.Metascore = random.Next(50, 100);
            newMovie.CriticReviews = random.Next(100, 500);
            await daoMovie.AddMovie(newMovie);
            Console.WriteLine("Movie added successfully.");
            // ceva cu imaginea
            // adaugare in baza de date + lista
            // binding in xaml

           // await Shell.Current.GoToAsync("..");
           await Shell.Current.GoToAsync("MoviesView");
        }


        [RelayCommand]
        void ToggleAction()
        {
            IsActionSelected = !IsActionSelected;
        }
        [RelayCommand]
        void ToggleAdventure()
        {
            IsAdventureSelected = !IsAdventureSelected;
        }
        [RelayCommand]
        void ToggleAnimation()
        {
            IsAnimationSelected = !IsAnimationSelected;
        }
        [RelayCommand]
        void ToggleComedy()
        {
            IsComedySelected = !IsComedySelected;
        }
        [RelayCommand]
        void ToggleCrime()
        {
            IsCrimeSelected = !IsCrimeSelected;
        }
        [RelayCommand]
        void ToggleDrama()
        {
            IsDramaSelected = !IsDramaSelected;
        }
        [RelayCommand]
        void ToggleFamily()
        {
            IsFamilySelected = !IsFamilySelected;
        }
        [RelayCommand]
        void ToggleHistory()
        {
            IsHistorySelected = !IsHistorySelected;
        }
        [RelayCommand]
        void ToggleHorror()
        {
            IsHorrorSelected = !IsHorrorSelected;
        }
        [RelayCommand]
        void ToggleMusic()
        {
            IsMusicSelected = !IsMusicSelected;
        }
        [RelayCommand]
        void ToggleMystery()
        {
            IsMysterySelected = !IsMysterySelected;
        }
        [RelayCommand]
        void ToggleRomance()
        {
            IsRomanceSelected = !IsRomanceSelected;
        }


        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
