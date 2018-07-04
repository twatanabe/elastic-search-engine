using ElasticSerchEngine.Config;
using ElasticSerchEngine.Models;
using ElasticSerchEngine.Services;
using Nest;
using System;
using System.Collections.Generic;
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
                .Aggregations(aggs => aggs
                    .Terms("by_tags", t => t
                        .Field(f => f.Tags).Size(10)))
                .Size(pageSize));

            return new SearchResult<Post>
            {
                Total = (int)result.Total,
                Page = page,
                Results = result.Documents,
                SearchMilliseconds = result.Took,
                AggregationsByTags = result.Aggregations.Terms("by_tags").Buckets.ToDictionary(x => x.Key, y => y.DocCount.GetValueOrDefault(0))
            };
        }

        public SearchResult<Post> SearchByCategory(string query, IEnumerable<string> tags, int page = 1, int pageSize = 10)
        {
            var filters = tags.Select(c => new Func<QueryContainerDescriptor<Post>, QueryContainer>(x => x.Term(f => f.Tags, c))).ToArray();

            var result = client.Search<Post>(x => x
                .Query(q => q
                    .Bool(b => b
                        .Must(m => m
                            .MultiMatch(mp => mp
                                .Query(query)
                                .Fields(f => f
                                .Fields(f1 => f1.Title, f2 => f2.Body, f3 => f3.Tags))))
                         .Filter(f => f
                            .Bool(b1 => b1
                                .Must(filters)))))
                .Aggregations(aggs => aggs
                    .Terms("by_tags", t => t
                        .Field(f => f.Tags).Size(10)))
                .Size(pageSize));

            return new SearchResult<Post>
            {
                Total = (int)result.Total,
                Page = page,
                Results = result.Documents,
                SearchMilliseconds = result.Took,
                AggregationsByTags = result.Aggregations.Terms("by_tags").Buckets.ToDictionary(x => x.Key, y => y.DocCount.GetValueOrDefault(0))
            }; ;
        }

        public IEnumerable<string> Autocomplete(string query)
        {
            var result = client.Search<Post>(s => s.Query(q => q.MultiMatch(mp => mp.Query(query)))
                .Suggest(sug => sug
                    .Completion("tag-suggestions", c => c.Field(f => f.Tags))));

            return result.Suggest["tag-suggestions"].SelectMany(x => x.Options).Select(y => y.Text);
        }

        public IEnumerable<string> Suggest(string query)
        {
            var result = client.Search<Post>(s => s.Query(q => q.MultiMatch(mp => mp.Query(query)))
                .Suggest(sug => sug
                    .Completion("post-suggestions", c => c.Field(f => f.Body).Field(f => f.Title).Field(f => f.Tags).Size(6))));

            return result.Suggest["post-suggestions"].SelectMany(x => x.Options).Select(y => y.Text);
        }

        public Post Get(string id)
        {
            var result = client.Get<Post>(new DocumentPath<Post>(id));
            return result.Source;
        }
    }
}
