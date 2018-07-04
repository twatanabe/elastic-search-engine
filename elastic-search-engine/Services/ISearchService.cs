using ElasticSerchEngine.Models;
using System.Collections.Generic;

namespace ElasticSerchEngine.Services
{
    public interface ISearchService<Post>
    {
        SearchResult<Post> Search(string query, int page = 1, int pageSize = 10);

        SearchResult<Post> SearchByCategory(string query, IEnumerable<string> tags, int page = 1, int pageSize = 10);

        IEnumerable<string> Autocomplete(string query);

        IEnumerable<string> Suggest(string query);

        Post Get(string id);
    }
}
