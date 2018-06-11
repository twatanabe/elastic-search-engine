using System.Threading.Tasks;

namespace ElasticSerchEngine.Services
{
    public interface IAzureBlobService
    {
        Task<string> GetDefaultXMLData();
    }
}
