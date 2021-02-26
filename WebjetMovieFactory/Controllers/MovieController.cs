using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebjetMovieFactory.Services;
using WebjetMovieFactory.Services.Interfaces;

namespace WebjetMovieFactory.Controllers
{
    [Route("api")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly MovieService _movieService;

        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("{source}/movies")]
        public IActionResult GetMovies(string source)
        {
            var movies = _movieService.GetMovies(source);

            if (movies == null)
            {
                return NotFound();
            }

            return Ok(movies);
        }

        [HttpGet("{source}/movie/{Id}")]
        public IActionResult GetMovieById(string source, int Id)
        {
            var movie = _movieService.GetMovieById(source, Id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

    }
}
