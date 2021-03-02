using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebjetMovieFactory.Controllers;
using WebjetMovieFactory.Entities.DataModels;
using WebjetMovieFactory.Services.Interfaces;
using Xunit;

namespace WebjetMovieFactoryTest
{
    public class MovieControllerUnitTest
    {
        [Fact]
        public async Task Test_GetMovies_OKAsync()
        {
            // Arrange
            var source = "cinemaworld";
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(service => service.GetMoviesAsync(source))
                .Returns(GetTestMovies(source));
            var logger = Mock.Of<ILogger<MovieController>>();

            var controller = new MovieController(mockMovieService.Object, logger);

            // Act
            var result = await controller.GetMovies(source);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = JsonConvert.DeserializeObject<List<Movie>>((string)okResult.Value);
            Assert.Equal(2, returnValue.Count);

            var movie = returnValue.FirstOrDefault();
            Assert.Equal(source, movie.Source);
            Assert.Equal("1", movie.ID);
        }


        [Fact]
        public async Task Test_GetMovieById_OKAsync()
        {
            // Arrange
            var source = "cinemaworld";
            var ID = "2";
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(service => service.GetMovieByIdAsync(source, ID))
                .Returns(GetTestMovie(source));
            var logger = Mock.Of<ILogger<MovieController>>();

            var controller = new MovieController(mockMovieService.Object, logger);

            // Act
            var result = await controller.GetMovieByIdAsync(source, ID);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var movie = JsonConvert.DeserializeObject<Movie>((string)okResult.Value);
            Assert.Equal(source, movie.Source);
            Assert.Equal("2", movie.ID);
        }

        [Fact]
        public async Task Test_GetMovieById_NotFoundAsync()
        {
            // Arrange
            var source = "filmworld";
            var ID = "2";
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(service => service.GetMovieByIdAsync(source, ID))
                .Returns(GetTestMovie(source));
            var logger = Mock.Of<ILogger<MovieController>>();

            var controller = new MovieController(mockMovieService.Object, logger);

            // Act
            var result = await controller.GetMovieByIdAsync(source, ID);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        public async Task<IList<Movie>> GetTestMovies(string source)
        {
            var movieList = new List<Movie>()
            {
                new Movie
                {
                    ID = "1",
                    Title = "Harry Potter and the Philosopher's Stone",
                    Year = "2001",
                    Source = "cinemaworld",
                    Price = "19.00"
                },
                new Movie
                {
                    ID = "2",
                    Title = "Harry Potter and the Chamber of Secrets",
                    Year = "2002",
                    Source = "cinemaworld",
                    Price = "29.00"
                }
            };

            return movieList.Where(m => m.Source == source)?.ToList();
        }

        public async Task<Movie> GetTestMovie(string source)
        {

            return new Movie()
            {
                ID = "2",
                Title = "Harry Potter and the Chamber of Secrets",
                Year = "2002",
                Source = "cinemaworld",
                Price = "29.00"
            };

        }
    }
}
