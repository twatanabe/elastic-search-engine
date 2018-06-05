using Nest;

namespace ElasticSerchEngine.Config
{
    public interface IElasticConfig
    {
        IElasticClient GetClient();
    }
}
