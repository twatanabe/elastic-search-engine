using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ElasticSerchEngine.Services
{
    public class FTPStorageService : IStorageService
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private string xmlData;

        public FTPStorageService(IConfiguration config, ILogger<AzureBlobService> logger)
        {
            _config = config;
            _logger = logger;

            LoadDefaultXMLData();
        }

        private void LoadDefaultXMLData()
        {
            try
            {
                // Get the object used to communicate with the server.  
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(_config["xmlFTPFileUrl"]);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                xmlData = reader.ReadToEnd();

                reader.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to load XML file. {ex}");
                throw ex;
            }
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
                _logger.LogTrace("Data featched and loaded");
            }
            else
            {
                _logger.LogTrace("Data already exists");
            }

            return xmlData;
        }
    }
}
