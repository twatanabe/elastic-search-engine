namespace ElasticSerchEngine.Services
{
    public interface IIndexService
    {
        bool IndexExists();
        void CreateIndex(string fileName, int maxItems);
    }
}
