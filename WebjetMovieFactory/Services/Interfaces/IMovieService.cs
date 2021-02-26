using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebjetMovieFactory.DataLayer.DataModels;

namespace WebjetMovieFactory.Services.Interfaces
{
    public interface IMovieService
    {
       IList<Movie> GetMovies(string source);

       Movie GetMovieById(string source, int id);

    }
}
