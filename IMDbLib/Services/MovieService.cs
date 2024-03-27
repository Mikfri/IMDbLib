using CsvHelper;
using IMDbLib.Models;
using IMDbLib.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
            _repository.RunStoredProcedure("AddMovie", movie.TitleType.Type, movie.PrimaryTitle, movie.IsAdult, movie.StartYear, movie.EndYear, movie.RuntimeMins);
        }

        public MovieBase GetMovieDetails(string tconst)
        {
            var movie = _repository
                .GetAll()
                .Include(m => m.Directors)
                    .ThenInclude(d => d.Person)
                .Include(m => m.Writers)
                    .ThenInclude(w => w.Person)
                .FirstOrDefault(m => m.Tconst == tconst);

            return movie;
        }

        //public MovieBase GetMovieDetails(string tconst)
        //{
        //    //return _repository.GetWithIncludes(tconst, m => m.Tconst == tconst, m => m.Directors, m => m.Writers);
        //    //return _repository.GetWithIncludes(tconst, m => m.Directors, m => m.Writers);

        //    return _repository.RunStoredProcedure("GetMovieDetails", tconst).Cast<MovieBase>().FirstOrDefault();
        //}

        //public MovieBase GetMovieDetails(string tconst)
        //{
        //    var movie = _repository.RunStoredProcedure("GetMovieBase", tconst).Cast<MovieBase>().FirstOrDefault();
        //    var directors = _repository.RunStoredProcedure("GetMovieRelatedDirectors", tconst).Cast<MovieDirector>().ToList();
        //    var writers = _repository.RunStoredProcedure("GetMovieRelatedWriters", tconst).Cast<MovieWriter>().ToList();

        //    // Tilføj directors og writers til movie objektet her...
        //    movie.Directors = directors;
        //    movie.Writers = writers;

        //    return movie;
        //}

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
