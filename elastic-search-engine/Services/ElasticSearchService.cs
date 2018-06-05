using ElasticSerchEngine.Config;
using ElasticSerchEngine.Models;
using ElasticSerchEngine.Services;
using Nest;

namespace ElasticSearchEngine.Services
{
    public class ElasticSearchService : ISearchService<Post>
    {
        private readonly IElasticClient client;

        public ElasticSearchService(ElasticConfig elasticConfig)
        {
            client = elasticConfig.GetClient();
        }

        public SearchResult<Post> Search(string query, int page, int pageSize)
        {
            var result = client.Search<Post>(
                x => x.Query(
                    q => q.MultiMatch(
                        mp => mp.Query(query))));

            return new SearchResult<Post>
            {
                Total = (int)result.Total,
                Page = page,
                Results = result.Documents
            };
        }
    }
}
