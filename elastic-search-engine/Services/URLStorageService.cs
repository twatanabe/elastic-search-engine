using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ElasticSerchEngine.Services
{
    public class URLStorageService : IStorageService
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private string xmlData;

        public URLStorageService(IConfiguration config, ILogger<AzureBlobService> logger)
        {
            _config = config;
            _logger = logger;

            LoadDefaultXMLData();
        }

        private void LoadDefaultXMLData()
        {
            //var xmlData = new MemoryStream(new WebClient().DownloadData(_config["xmlFileUrl"]));

            byte[] data;
            using (var stream = new MemoryStream())
            {
                using (var client = new WebClient())
                {
                    data = client.DownloadData(_config["xmlFileUrl"]);
                }
            }

            xmlData = System.Text.Encoding.Default.GetString(data);

            //using (var client = new WebClient())
            //{
            //    client.Credentials = new NetworkCredential("UserName", "Password");
            //    client.DownloadFile(_config["xmlFileUrl"], "");
            //}
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
