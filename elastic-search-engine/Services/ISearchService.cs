using ElasticSerchEngine.Models;
using System.Collections.Generic;

namespace ElasticSerchEngine.Services
{
    public interface ISearchService<Post>
    {
        SearchResult<Post> Search(string query, int page = 1, int pageSize = 10);
    }
}
