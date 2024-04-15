using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbLib.Models
{
    public class Profession
    {
        [Key]
        [Name("primaryProfession")]
        public string? PrimaryProfession { get; set; }

        //public ICollection<PersonalCareer> PersonalCareers { get; set; } = new List<PersonalCareer>();
        public Profession() { }

        public override string ToString()
        {
            return $"{PrimaryProfession}";
        }
    }
}
