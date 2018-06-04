using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nest;
using System;

namespace ElasticSerchEngine.Services
{
    public class ElasticConfig : IElasticConfig
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public ElasticConfig(IConfiguration configuration, ILogger<ElasticConfig> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public IElasticClient GetClient()
        {
            _logger.LogInformation(1, "I Have The Power!!!!!!!!");
            Uri node = new Uri(ElasticsearchUrl);
            ConnectionSettings settings = new ConnectionSettings(node);
            settings.DefaultIndex(IndexName);
            return new ElasticClient(settings);
        }

        public string IndexName
        {
            get { return _configuration["indexName"]; }
        }

        public string ElasticsearchUrl
        {
            get { return _configuration["elasticsearchUrl"]; }
        }
    }
}
