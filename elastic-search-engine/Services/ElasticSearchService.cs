using ElasticSerchEngine.Config;
using ElasticSerchEngine.Models;
using ElasticSerchEngine.Services;
using Nest;
using System.Linq;

namespace ElasticSearchEngine.Services
{
    public class ElasticSearchService : ISearchService<Post>
    {
        private readonly IElasticClient client;

        public ElasticSearchService(IElasticConfig elasticConfig)
        {
            client = elasticConfig.GetClient();
        }

        public SearchResult<Post> Search(string query, int page, int pageSize)
        {
            var result = client.Search<Post>(x => x
                .Query(q => q
                    .MultiMatch(mp => mp
                        .Query(query)
                            .Fields(f => f
                                .Fields(f1 => f1.Title, f2 => f2.Body, f3 => f3.Tags))))
                    //.Aggregations(a => a
                    //    .Terms("by_tags", t => t
                    //        .Field(f => f
                    //            .Tags).Size(10)))
                .Size(pageSize));

            return new SearchResult<Post>
            {
                Total = (int)result.Total,
                Page = page,
                Results = result.Documents,
                SearchMilliseconds = result.Took,
                //AggregationsByTags = result.Aggregations.Terms("by_tags").Buckets.ToDictionary(x => x.Key, y => y.DocCount.GetValueOrDefault(0))
            };
        }

        public Post Get(string id)
        {
            var result = client.Get<Post>(new DocumentPath<Post>(id));
            return result.Source;
        }
    }
}
