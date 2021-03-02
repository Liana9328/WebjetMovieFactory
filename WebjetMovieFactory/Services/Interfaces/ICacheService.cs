using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebjetMovieFactory.Services.Interfaces
{
    public interface ICacheService
    {
        T GetFromCache<T>(string key) where T : class;
        void SetCache<T>(string key, T value) where T : class;
        void ClearCache(string key);
    }
}
