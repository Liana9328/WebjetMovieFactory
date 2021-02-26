using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebjetMovieFactory.DataLayer.DataModels;
using WebjetMovieFactory.DataLayer.Interfaces;
using WebjetMovieFactory.Services.Interfaces;

namespace WebjetMovieFactory.Services
{
    public class MovieService: IMovieService
    {
        private readonly IDataAccessor _dataAccessor;

        public MovieService(IDataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;
        }

        public IList<Movie> GetMovies(string source)
        {
            var cinemaWorldMovies = _dataAccessor.GetMovies(source);

            return cinemaWorldMovies;
        }

        public Movie GetMovieById(string source, int id)
        {
            var cinemaWorldMovies = _dataAccessor.GetMovies(source);

            var selectedMovie = cinemaWorldMovies?.FirstOrDefault(x => x.Id == id);

            return selectedMovie;
        }

    }
}
