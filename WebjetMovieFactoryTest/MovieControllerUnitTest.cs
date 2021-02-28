using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using WebjetMovieFactory.Controllers;
using WebjetMovieFactory.DataLayer.DataModels;
using WebjetMovieFactory.Services;
using WebjetMovieFactory.Services.Interfaces;
using Xunit;

namespace WebjetMovieFactoryTest
{
    public class MovieControllerUnitTest
    {
        [Fact]
        public void Test_GetMovies_OK()
        {
            // Arrange
            var source = "cinemaworld";
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(service => service.GetMovies(source))
                .Returns(GetTestMovies(source));
            var logger = Mock.Of<ILogger<MovieController>>();

            var controller = new MovieController(mockMovieService.Object, logger);

            // Act
            var result = controller.GetMovies(source);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = JsonConvert.DeserializeObject<List<Movie>>((string)okResult.Value);
            Assert.Equal(2, returnValue.Count);

            var movie = returnValue.FirstOrDefault();
            Assert.Equal(source, movie.Source);
            Assert.Equal(1, movie.Id);
        }


        [Fact]
        public void Test_GetMovieById_OK()
        {
            // Arrange
            var source = "cinemaworld";
            var Id = 2;
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(service => service.GetMovieById(source, Id))
                .Returns(GetTestMovies(source).FirstOrDefault(x => x.Id == Id));
            var logger = Mock.Of<ILogger<MovieController>>();

            var controller = new MovieController(mockMovieService.Object, logger);

            // Act
            var result = controller.GetMovieById(source, Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var movie = JsonConvert.DeserializeObject<Movie>((string)okResult.Value);
            Assert.Equal(source, movie.Source);
            Assert.Equal(2, movie.Id);
        }

        [Fact]
        public void Test_GetMovieById_NotFound()
        {
            // Arrange
            var source = "filmworld";
            var Id = 2;
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(service => service.GetMovieById(source, Id))
                .Returns(GetTestMovies(source).FirstOrDefault(x => x.Id == Id));
            var logger = Mock.Of<ILogger<MovieController>>();

            var controller = new MovieController(mockMovieService.Object, logger);

            // Act
            var result = controller.GetMovieById(source, Id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        public IList<Movie> GetTestMovies(string source)
        {
            var movieList = new List<Movie>()
            {
                new Movie
                {
                    Id = 1,
                    Name = "Harry Potter and the Philosopher's Stone",
                    Year = "2001",
                    Source = "cinemaworld",
                    Price = decimal.Parse("19.00")
                },
                new Movie
                {
                    Id = 2,
                    Name = "Harry Potter and the Chamber of Secrets",
                    Year = "2002",
                    Source = "cinemaworld",
                    Price = decimal.Parse("29.00")
                }
            };

            return movieList.Where(m => m.Source == source)?.ToList();
        }
    }
}
