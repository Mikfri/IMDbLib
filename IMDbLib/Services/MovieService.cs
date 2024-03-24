using IMDbLib.Models;
using IMDbLib.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbLib.Services
{
    public class MovieService
    {
        private readonly IGRepository<MovieBase> _repository;

        public MovieService(IGRepository<MovieBase> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IEnumerable<MovieBase> SearchMovies(string title)
        {
            return _repository.RunStoredProcedure("SearchMovies", title).Cast<MovieBase>();
        }
        public void AddMovie(MovieBase movie)
        {
            _repository.RunStoredProcedure("AddMovie", movie.TitleType, movie.PrimaryTitle, movie.IsAdult, movie.StartYear, movie.EndYear, movie.RuntimeMins);
        }

        public MovieBase GetMovieDetails(string tconst)
        {
            return _repository.RunStoredProcedure("GetMovieDetails", tconst).Cast<MovieBase>().FirstOrDefault();
        }

        public MovieBase UpdateMovie(MovieBase movie)
        {
            _repository.RunStoredProcedure("UpdateMovie", movie.Tconst, movie.TitleType, movie.PrimaryTitle, movie.IsAdult, movie.StartYear, movie.EndYear, movie.RuntimeMins);
            return _repository.Get(movie.Tconst);
        }

        public void DeleteMovie(MovieBase movie)
        {
            _repository.RunStoredProcedure("DeleteMovie", movie.Tconst);
        }
    }
}
