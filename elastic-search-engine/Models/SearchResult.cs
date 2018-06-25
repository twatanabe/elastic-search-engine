using System.Collections.Generic;

namespace ElasticSerchEngine.Models
{
    public class SearchResult<T>
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public IEnumerable<T> Results { get; set; }
        public long SearchMilliseconds { get; set; }
        public Dictionary<string, long> AggregationsByTags { get; set; }
    }
}
