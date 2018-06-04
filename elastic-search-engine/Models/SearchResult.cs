using System.Collections.Generic;

namespace ElasticSerchEngine.Models
{
    public class SearchResult<T>
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
