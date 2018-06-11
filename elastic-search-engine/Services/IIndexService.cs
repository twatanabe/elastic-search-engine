namespace ElasticSerchEngine.Services
{
    public interface IIndexService
    {
        bool IndexExists();
        void CreateIndex(int maxItems);
    }
}
