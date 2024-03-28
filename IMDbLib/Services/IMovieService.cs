using IMDbLib.DTOs;
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
        Task AddMovie(MovieBaseDTO movieDTO);
        Task<List<MovieBaseDTO>> GetMovieListByTitle(string searchString);
        Task<AllMovieInfoDTO> GetAllMovieInfoByTconst(string tconst);
        Task UpdateMovie(MovieBaseDTO movieDTO);
        Task DeleteMovie(string tconst);
    }
}
