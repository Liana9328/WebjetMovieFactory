using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebjetMovieFactory.DataLayer.DataModels;

namespace WebjetMovieFactory.DataLayer.Interfaces
{
    public interface IDataAccessor
    {
        IList<Movie> GetMovies(string source);

    }
}
