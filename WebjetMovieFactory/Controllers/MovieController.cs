using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebjetMovieFactory.Controllers.ActionFilter;
using WebjetMovieFactory.Services;
using WebjetMovieFactory.Services.Interfaces;

namespace WebjetMovieFactory.Controllers
{
    [Route("api")]
    [ApiController]
    [ServiceFilter(typeof(TokenAuthenticate))]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<MovieController> _logger;

        public MovieController(IMovieService movieService, ILogger<MovieController> logger)
        {
            _movieService = movieService;
            _logger = logger;
        }

        [HttpGet("{source}/movies")]
        public IActionResult GetMovies(string source)
        {
            var movies = _movieService.GetMovies(source);

            if (movies == null)
            {
                _logger.LogError($"No movies returned from {source}");

                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(movies));
        }

        [HttpGet("{source}/movie/{Id}")]
        public IActionResult GetMovieById(string source, int Id)
        {
            var movie = _movieService.GetMovieById(source, Id);

            if (movie == null)
            {
                _logger.LogError($"Movie {Id} not returned from {source}");

                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(movie));
        }

    }
}
