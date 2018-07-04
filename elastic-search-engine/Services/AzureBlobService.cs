using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;

namespace ElasticSerchEngine.Services
{
    public class AzureBlobService : IStorageService
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private string xmlData;

        public AzureBlobService(IConfiguration config, ILogger<AzureBlobService> logger)
        {
            _config = config;
            _logger = logger;

            LoadDefaultXMLData();
        }

        private void LoadDefaultXMLData()
        {
            var storageAccount = CloudStorageAccount.Parse(_config["StorageConnection"]);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("default");
            var blob = container.GetBlockBlobReference("Posts.xml");

            xmlData = blob.DownloadTextAsync().Result;
        }

        public void DeleteXMLDataMemory()
        {
            xmlData = null;
        }

        public string GetDefaultXMLData()
        {
            if (xmlData == null)
            {
                LoadDefaultXMLData();
            }

            return xmlData;
        }
    }
}
