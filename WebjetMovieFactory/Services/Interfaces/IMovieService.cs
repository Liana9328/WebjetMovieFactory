using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebjetMovieFactory.Entities.DataModels;

namespace WebjetMovieFactory.Services.Interfaces
{
    public interface IMovieService
    {
        Task<IList<Movie>> GetMoviesAsync(string source);

        Task<Movie> GetMovieByIdAsync(string source, string id);

    }
}
