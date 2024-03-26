using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbLib.Models
{
    public class MovieBase
    {
        [Key]
        [Name("tconst")]
        public string Tconst { get; set; }

        [Name("titleType")]
        public TitleType TitleType { get; set; }

        [Name("primaryTitle")]
        public string PrimaryTitle { get; set; }

        [Name("originalTitle")]
        public string? OriginalTitle { get; set; }

        [Name("isAdult")]
        public bool IsAdult { get; set; }

        [Ignore]
        public DateOnly? StartYear { get; set; }

        [Ignore]
        public DateOnly? EndYear { get; set; }

        [Name("runtimeMinutes")]
        public int? RuntimeMins { get; set; }


        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
        public ICollection<MovieDirector> Directors { get; set; } = new List<MovieDirector>();
        public ICollection<MovieWriter> Writers { get; set; } = new List<MovieWriter>();

        /*
        VIRTUAL NAVIGATION PROPERTIES
        When using lazy loading proxies, all navigation properties must be declared as virtual.
        This allows Entity Framework to override them in a derived proxy class 
        and insert the code necessary to perform lazy loading.
         */

        public MovieBase() { }

        public override string ToString()
        {
            return $"{Tconst}, {PrimaryTitle}, {OriginalTitle}, {IsAdult}, {StartYear}, {EndYear}, {RuntimeMins}";
        }
    }
}
