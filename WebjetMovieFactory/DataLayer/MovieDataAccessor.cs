using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebjetMovieFactory.DataLayer.DataModels;
using WebjetMovieFactory.DataLayer.Interfaces;

namespace WebjetMovieFactory.DataLayer
{
    public class MovieDataAccessor: IDataAccessor
    {
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
                //Log
                return null;
            }
        }
    }
}
