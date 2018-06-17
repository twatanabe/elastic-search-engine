namespace ElasticSerchEngine.Services
{
    public interface IIndexService
    {
        bool CanBeQueried();
        bool IndexExists();
        void CreateIndex(int maxItems);
    }
}
