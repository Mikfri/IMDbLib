using IMDbLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbLib.Services
{
    public interface IMovieService
    {
        IEnumerable<MovieBase> SearchMovies(string title);
        void AddMovie(MovieBase movie);
        MovieBase GetMovieDetails(string tconst);
        MovieBase UpdateMovie(MovieBase movie);
        void DeleteMovie(MovieBase movie);
    }
}
