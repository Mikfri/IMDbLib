using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IMDbLib.Models
{
    public class PersonalCareer
    {
        [Key]
        [ForeignKey("Person")]
        public string Nconst { get; set; }
        public Person Person { get; set; }


        [ForeignKey("Profession")]
        public string? PrimProf { get; set; }
        public Profession Profession { get; set; }


        public PersonalCareer() { }

    }
}
