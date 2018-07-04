using System.Threading.Tasks;

namespace ElasticSerchEngine.Services
{
    public interface IStorageService
    {

        //void LoadDefaultXMLData();
        void DeleteXMLDataMemory();
        string GetDefaultXMLData();
    }
}
