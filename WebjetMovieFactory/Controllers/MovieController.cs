using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebjetMovieFactory.Controllers.ActionFilter;
using WebjetMovieFactory.DataLayer.DataModels;
using WebjetMovieFactory.Services;
using WebjetMovieFactory.Services.Interfaces;

namespace WebjetMovieFactory.Controllers
{
    [Route("api")]
    [ApiController]
    [ServiceFilter(typeof(ResponseExceptionFilter))]
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
        public async Task<IActionResult> GetMovies(string source)
        {
            var movies = await _movieService.GetMoviesAsync(source);

            if (movies.Count < 1)
            {
                _logger.LogError($"No movies returned from {source}");

                return NotFound();
            }
            
            return Ok(JsonConvert.SerializeObject(movies));

        }

        [HttpGet("{source}/movie/{Id}")]
        public async Task<IActionResult> GetMovieByIdAsync(string source, string Id)
        {
            var movie = await _movieService.GetMovieByIdAsync(source, Id);

            if (movie.Source != source || movie.ID != Id)
            {
                _logger.LogError($"Movie {Id} not returned from {source}");

                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(movie));
        }

    }
}
