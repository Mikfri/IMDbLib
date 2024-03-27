using CsvHelper;
using IMDbLib.DataContext;
using IMDbLib.DTOs;
using IMDbLib.Models;
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
    /// <summary>
    /// MovieService klassen har direkte adgang til DbContext og benytter ikke en repository klasse.
    /// Dette skyldes vi benytter Entity Framework Core, som er en repository og unit of work i sig selv.
    /// 
    /// Brugeren har IKKE direkte adgang til tabellerne i databasen, fordi alt kommunikation med databasen
    /// sker gennem service klassen.
    /// </summary>
    public class MovieService
    {
        private readonly IMDb_Context _context;

        public MovieService(IMDb_Context context)
        {
            _context = context;
        }

        public async Task AddMovie(MovieBaseDTO movieDTO)
        {
            //-------- Automatisk generering af ID --------
            // Find the highest existing ID
            var highestId = await _context.MovieBases.MaxAsync(m => m.Tconst);

            // Extract the numeric part of the ID and increment it
            int numericPart = int.Parse(highestId.Substring(2));
            numericPart++;

            // Construct the new ID
            string newId = "tt" + numericPart.ToString("D7"); // D7 means 7 digits with leading zeros

            //-------- Opret MovieBase objekt --------
            // Check if the TitleType exists, if not create it
            var titleType = await _context.TitleTypes.FindAsync(movieDTO.TitleType) ?? new TitleType { Type = movieDTO.TitleType };

            // Create the MovieBase object
            MovieBase movie = new MovieBase
            {
                Tconst = newId,
                TitleType = titleType,
                PrimaryTitle = movieDTO.PrimaryTitle,
                OriginalTitle = movieDTO.OriginalTitle,
                IsAdult = movieDTO.IsAdult,
                StartYear = movieDTO.StartYear,
                EndYear = movieDTO.EndYear,
                RuntimeMins = movieDTO.RuntimeMins
            };

            // Check if the Genres exist, if not create them and add them to the MovieBase
            foreach (var genre in movieDTO.Genres)
            {
                var movieGenre = await _context.Genres.FindAsync(genre) ?? new Genre { GenreType = genre };
                movie.MovieGenres.Add(new MovieGenre { MovieBase = movie, Genre = movieGenre });
            }

            _context.MovieBases.Add(movie);
            await _context.SaveChangesAsync();
        }
    }
}
