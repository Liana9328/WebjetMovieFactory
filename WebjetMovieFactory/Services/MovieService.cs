using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebjetMovieFactory.Entities.DataModels;
using WebjetMovieFactory.Services.Interfaces;

namespace WebjetMovieFactory.Services
{
    public class MovieService: IMovieService
    {
        private readonly ICacheService _cacheService;
        private readonly IConfiguration _config;
        private readonly string _token;

        public MovieService(IConfiguration config, ICacheService cacheService)
        {
            _cacheService = cacheService;

            _config = config;
            _token = _config.GetValue<string>("Token");
        }

        public async Task<IList<Movie>> GetMoviesAsync(string source)
        {
            var movieList = new List<Movie>();
            var cachedMovies = _cacheService.GetFromCache<IList<Movie>>(source);

            if (cachedMovies != null)
            {
                return cachedMovies;
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-Access-Token", _token);

                using (var response = await httpClient?.GetAsync($"http://webjetapitest.azurewebsites.net/api/{source}/movies"))
                {
                    var strResponse = await response?.Content.ReadAsStringAsync();

                    DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(strResponse);

                    var dataTable = dataSet?.Tables["Movies"];
                    var dataRows = dataTable?.Rows;

                    if (dataRows != null)
                    {
                        foreach (DataRow row in dataRows)
                        {
                            movieList.Add(new Movie()
                            {
                                Title = row["Title"].ToString(),
                                Year = row["Year"].ToString(),
                                ID = row["ID"].ToString(),
                                Type = row["Type"].ToString(),
                                Poster = row["Poster"].ToString(),
                                Source = source
                            });
                        }
                    }
                }
            }

            _cacheService.SetCache<IList<Movie>>(source, movieList);

            return movieList;
        }

        public async Task<Movie> GetMovieByIdAsync(string source, string id)
        {
            var selectedMovie = new Movie();
            var cachedMovie = _cacheService.GetFromCache<Movie>(id);

            if (cachedMovie != null)
            {
                return cachedMovie;
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-Access-Token", _token);

                using (var response = await httpClient?.GetAsync($"http://webjetapitest.azurewebsites.net/api/{source}/movie/{id}"))
                {
                    var strResponse = await response?.Content.ReadAsStringAsync();
                    selectedMovie = JsonConvert.DeserializeObject<Movie>(strResponse);

                    if(selectedMovie != null)
                    {
                        selectedMovie.Source = source;
                    }                  
                }
            }

            _cacheService.SetCache<Movie>(id, cachedMovie);

            return selectedMovie;
        }
    }
}
