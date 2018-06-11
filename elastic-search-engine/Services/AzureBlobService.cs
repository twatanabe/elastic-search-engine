using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;

namespace ElasticSerchEngine.Services
{
    public class AzureBlobService : IAzureBlobService
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private Task<string> data;

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

            data = blob.DownloadTextAsync();
        }

        public async Task<string> GetDefaultXMLData()
        {
            if (data == null)
            {
                LoadDefaultXMLData();
            }

            return data.Result;
        }
    }
}
