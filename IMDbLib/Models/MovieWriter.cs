﻿using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbLib.Models
{
    public class MovieWriter
    {
        [Key]
        [Name("tconst")]
        public string Tconst { get; set; }
        public MovieBase MovieBase { get; set; }

        [ForeignKey("Person")]
        [Name("writers")]
        public string? Nconst { get; set; }
        public Person? Person { get; set; }
    }
}
