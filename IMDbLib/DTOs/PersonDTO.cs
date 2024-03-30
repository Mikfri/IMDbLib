using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbLib.DTOs
{
    public record PersonDTO
    {
        public string Nconst { get; init; }
        public string PrimaryName { get; init; }
        public DateOnly? BirthYear { get; init; }
        public DateOnly? DeathYear { get; init; }
        public List<string> PrimaryProfessions { get; init; } = new List<string>();        
    }
}
