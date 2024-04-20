using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinemate.Models
{
    public class DaoMovie
    {
        static DaoMovie daoMovie;
        SQLiteAsyncConnection connAsync;

        private DaoMovie()
        {
            connAsync = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "movie_list.db"));
            connAsync.CreateTableAsync<MovieLibrary>().Wait();
        }

        public static DaoMovie GetDaoMovie()
        {
            if (daoMovie == null)
            {
                daoMovie = new DaoMovie();
            }

            return daoMovie;

        }

        /*private async Task InitializeDatabaseAsync()
        {
             var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "movie_list.db");
             conn = new SQLiteAsyncConnection(dbPath);
             conn.CreateTableAsync<Movie>().Wait();
        }*/



        public async Task<int> AddMovie(MovieLibrary movie)
        {
            try
            {
                return await connAsync.InsertAsync(movie);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding movie to database: {ex.Message}");
                return 0;
            }
        }

        public async Task<int> DeleteMovie(MovieLibrary movie)
        {
            try
            {
                return await connAsync.DeleteAsync(movie);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding movie to database: {ex.Message}");
                return 0;
            }
        }

        public async Task<int> DeleteAllMovie()
        {
            try
            {
                return await connAsync.DeleteAllAsync<Movie>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding movie to database: {ex.Message}");
                return 0;
            }
        }


        public Task<List<MovieLibrary>> GetMovies()
        {
            return connAsync.QueryAsync<MovieLibrary>("SELECT * FROM movie_list");
        }

        //public async Task<int> AddMovieList(List<Movie> movieList)
        //{
        //    return await conn.InsertAllAsync(movieList);
        //}

    }

}
