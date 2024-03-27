using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbLib.DTOs
{
    public record MovieBaseDTO
    {

        public string TitleType { get; init; }
        public string PrimaryTitle { get; init; }
        public bool IsAdult { get; init; }
        public DateOnly StartYear { get; init; }
        public DateOnly? EndYear { get; init; }
        public int RuntimeMins { get; init; }
    }
}
