using ElasticSerchEngine.Config;
using Nest;

namespace ElasticSerchEngine.Services
{
    public class ElasticIndexService : IIndexService
    {
        private readonly IElasticClient client;
        private readonly IElasticConfig _elasticConfig;

        public ElasticIndexService(IElasticConfig elasticConfig)
        {
            _elasticConfig = elasticConfig;
            client = _elasticConfig.GetClient();
        }

        public void CreateIndex(string fileName, int maxItems)
        {
            //if (!client.IndexExists(_elasticConfig.IndexName).Exists)
            //{
            //    var indexDescriptor = new CreateIndexDescriptor(_elasticConfig.IndexName)
            //        .Mappings(ms => ms.Map<Post>(m => m.AutoMap()));

            //    client.CreateIndex(_elasticConfig.IndexName, i => indexDescriptor);
            //}
        }
    }
}
