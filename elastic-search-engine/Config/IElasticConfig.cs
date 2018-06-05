using Nest;

namespace ElasticSerchEngine.Config
{
    public interface IElasticConfig
    {
        string IndexName { get; }
        string ElasticsearchUrl { get; }

        IElasticClient GetClient();
    }
}
