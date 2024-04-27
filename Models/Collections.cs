namespace Cinemate.Models
{
    public class Collections
    {
        public static List<string> GetFilterOptions()
        {
            return new List<string> { "A - Z", "Z - A", "Score", "Upcoming", "Past" };
        }

        public static List<string> GetMovieSources()
        {
            return new List<string> { "All", "Watching", "Completed", "On Hold", "Dropped", "Plan to Watch" };
        }

        public static IEnumerable<string> GetMovieCategories()
        {
            return new List<string> { "Action", "Adventure", "Animation", "Comedy", "Crime", "Drama", 
                "Family", "History", "Horror", "Music", "Mystery", "Romance" };
            
        }

        public static IEnumerable<MovieLibrary> GetMovies()
        {
            return new List<MovieLibrary>
            {
                new MovieLibrary { Title = "Avatar", Cover = ImageConverter.ConvertImageToBase64("Cinemate.Resources.Images.avatar.jpg"), Rating = 9.5, Reviews = 3000, Metascore = 92, CriticReviews = 250, StartDate = "20-12-2023", Status = "Completed", Summary = "A paraplegic Marine dispatched to the moon Pandora on a unique mission becomes torn between following his orders and protecting the world he feels is his home.", MyReview = "Avatar is a visually stunning marvel of filmmaking that immerses you completely with its vivid, lush world of Pandora. The narrative echoes with themes of ecological harmony and cultural understanding, making it a powerful, resonant film that is just as relevant today as it was at release.", Categories = new List<string> { "Action", "Adventure", "Drama" } },
                new MovieLibrary { Title = "Ford v Ferrari", Cover = ImageConverter.ConvertImageToBase64("Cinemate.Resources.Images.ford_v_ferrari.jpg"), Rating = 8.2, Reviews = 2200, Metascore = 86, CriticReviews = 210, StartDate = "15-11-2021", Status = "Completed", Summary = "American car designer Carroll Shelby and driver Ken Miles battle corporate interference and the laws of physics to build a revolutionary race car for Ford in order to defeat Ferrari at the 24 Hours of Le Mans in 1966.", MyReview = "Ford v Ferrari delivers an exhilarating mix of high-octane racing sequences and heartwarming underdog story. Christian Bale and Matt Damon bring their A-game, translating the passion and persistence of their characters in a riveting, emotional journey that keeps you hooked till the checkered flag.", Categories = new List<string> { "Action", "Biography", "Drama" } },
                new MovieLibrary { Title = "Inside Out", Cover = ImageConverter.ConvertImageToBase64("Cinemate.Resources.Images.insideout.jpg"), Rating = 8.9, Reviews = 2800, Metascore = 88, CriticReviews = 300, StartDate = "19-06-2022", Status = "Completed", Summary = "After young Riley is uprooted from her Midwest life and moved to San Francisco, her emotions - Joy, Fear, Anger, Disgust and Sadness - conflict on how best to navigate a new city, house, and school.", MyReview = "Inside Out is a brilliant exploration of the emotional psyche, offering deep insights into the complex interplay of emotions within us all. It's a compelling narrative that is both entertaining and enlightening, making it a must-watch for both children and adults. The film cleverly balances humor and pathos, creating a rich, layered experience that resonates on many levels.", Categories = new List<string> { "Animation", "Comedy", "Drama" } }
            };
        }

        public static List<string> MovieStatuses => new List<string>
        {
            "Watching", "Completed", "On Hold", "Dropped", "Plan to Watch"
        };

        public static List<string> NumberList => new List<string>
        {
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"
        };

        public static Dictionary<string, int> GenreDictionary => new Dictionary<string, int>
        {
            { "Action", 28 },
            { "Adventure", 12 },
            { "Animation", 16 },
            { "Comedy", 35 },
            { "Crime", 80 },
            { "Documentary", 99 },
            { "Drama", 18 },
            { "Family", 10751 },
            { "Fantasy", 14 },
            { "History", 36 },
            { "Horror", 27 },
            { "Music", 10402 },
            { "Mystery", 9648 },
            { "Romance", 10749 },
            { "Science Fiction", 878 },
            { "TV Movie", 10770 },
            { "Thriller", 53 },
            { "War", 10752 },
            { "Western", 37 }
        };

        public static List<string> YearsList
        {
            get
            {
                var years = new List<string>();
                for (int year = DateTime.Now.Year; year >= 2014; year--)
                {
                    years.Add(year.ToString());
                }
                return years;
            }
        }

        public static Dictionary<string, string> SortByDictionary => new Dictionary<string, string>
        {
            {"Popularity", "popularity.desc" },
            { "Release date (ascending)", "primary_release_date.asc" },
            {"Release date (descending)", "primary_release_date.desc" },
            {"Number of votes", "vote_count.desc" }
        };

        public static IEnumerable<string> GetImgCollection01()
        {
            return new List<string>
            {
                "image1.png", "image2.png", "image3.png", "image4.png", "image5.png", "image6.png", "image7.png"
            };
        }

        public static IEnumerable<string> GetImgCollection02()
        {
            return new List<string>
            {
                "image8.png", "image9.png", "image10.png", "image11.png", "image12.png", "image13.png", "image14.png"
            };
        }

        public static IEnumerable<string> GetImgCollection03()
        {
            return new List<string>
            {
                "image15.png", "image16.png", "image17.png", "image18.png", "image19.png", "image20.png", "image21.png"
            };
        }

        public static Dictionary<string, bool> CategorySelections => new Dictionary<string, bool>
            {
                { "Action", false },
                { "Adventure", false },
                { "Animation", false },
                { "Comedy", false },
                { "Crime", false },
                { "Drama", false },
                { "Family", false },
                { "History", false },
                { "Horror", false },
                { "Music", false },
                { "Mystery", false },
                { "Romance", false }
        };
    }
}