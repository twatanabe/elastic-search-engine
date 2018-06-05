namespace ElasticSerchEngine.Services
{
    public interface IIndexService
    {
        void CreateIndex(string fileName, int maxItems);
    }
}
