using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebjetMovieFactory.DataLayer.DataModels;
using WebjetMovieFactory.DataLayer.Interfaces;

namespace WebjetMovieFactory.DataLayer
{
    public class MovieDataAccessor: IDataAccessor
    {
        private readonly ILogger<MovieDataAccessor> _logger;

        public MovieDataAccessor(ILogger<MovieDataAccessor> logger)
        {
            _logger = logger;
        }

        public IList<Movie> GetMovies(string source)
        {
            try
            {
                var myMovieString = File.ReadAllText("MockedData.json");

                var myMovieObject = JsonConvert.DeserializeObject<List<Movie>>(myMovieString);

                return myMovieObject?.Where(x => x.Source == source).ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error fetching movies from {source}. Exception {ex.Message}");

                return null;
            }
        }
    }
}
