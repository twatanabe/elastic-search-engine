using ElasticSerchEngine.Config;
using ElasticSerchEngine.Models;
using ElasticSerchEngine.Util;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ElasticSerchEngine.Services
{
    public class ElasticIndexService : IIndexService
    {
        private readonly IElasticClient client;
        private readonly IElasticConfig _elasticConfig;
        private readonly IStorageService _storageService;
        private readonly ILogger _logger;

        public ElasticIndexService(
            IElasticConfig elasticConfig, 
            IStorageService storageService, 
            ILogger<ElasticIndexService> logger)
        {
            _elasticConfig = elasticConfig;
            client = _elasticConfig.GetClient();

            _storageService = storageService;

            _logger = logger;
        }

        public bool CanBeQueried()
        {
            var response = client.Search<Post>(s => s);
            return response.IsValid;
        }

        public bool IndexExists()
        {
            var response = client.IndexExists(_elasticConfig.IndexName).Exists;

            _logger.LogTrace($"Index Exists - {response}");

            return response;
        }

        public void CreateIndex(int maxItems)
        {
            if (!IndexExists())
            {
                try
                {
                    _logger.LogTrace("No Index Nope");
                    var indexDescriptor = new CreateIndexDescriptor(_elasticConfig.IndexName)
                        .Mappings(ms => ms.Map<Post>(m => m.AutoMap()));

                    var indexResponse = client.CreateIndex(_elasticConfig.IndexName, idx => indexDescriptor);
                    _logger.LogTrace($"Create Index - {indexResponse.IsValid}");


                    //var response = client.Bulk(b => b.Index<Post>(idx => idx.Document(new Post { })));

                    //_logger.LogTrace($"Bulk Index - {response.IsValid}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed creating Index. {ex}");
                    throw ex;
                }
            }

            int take = maxItems;
            int batch = 1000;

            var defaultXMLData = _storageService.GetDefaultXMLData();

            foreach (var batches in LoadPostsFromData(defaultXMLData).Take(take).DoBatch(batch))
            {
                var result = client.IndexMany<Post>(batches, _elasticConfig.IndexName);
            }

            //foreach (var batches in LoadPostsFromFile("Data/Posts.xml").Take(take).DoBatch(batch))
            //{
            //    i++;
            //    var result = client.IndexMany<Post>(batches, _elasticConfig.IndexName);
            //}
        }

        //private IEnumerable<Post> LoadPostsFromFile(string inputUrl)
        //{
        //    using (XmlReader reader = XmlReader.Create(inputUrl))
        //    {
        //        reader.MoveToContent();
        //        while (reader.Read())
        //        {
        //            if (reader.NodeType == XmlNodeType.Element && reader.Name == "row")
        //            {
        //                if (String.Equals(reader.GetAttribute("PostTypeId"), "1"))
        //                {
        //                    XElement el = XNode.ReadFrom(reader) as XElement;

        //                    if (el != null)
        //                    {
        //                        Post post = new Post
        //                        {
        //                            Id = el.Attribute("Id").Value,
        //                            Title = el.Attribute("Title") != null ? el.Attribute("Title").Value : "",
        //                            Body = HtmlRemoval.StripTagsRegex(el.Attribute("Body").Value),
        //                        };
        //                        yield return post;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        private IEnumerable<Post> LoadPostsFromData(string data)
        {
            string _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (data.StartsWith(_byteOrderMarkUtf8))
            {
                data = data.Remove(0, _byteOrderMarkUtf8.Length);
            }

            using (XmlReader reader = XmlReader.Create(new StringReader(data)))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "row")
                    {
                        if (String.Equals(reader.GetAttribute("PostTypeId"), "1"))
                        {
                            XElement el = XNode.ReadFrom(reader) as XElement;

                            if (el != null)
                            {
                                Post post = new Post
                                {
                                    Id = el.Attribute("Id").Value,
                                    Title = el.Attribute("Title") != null ? el.Attribute("Title").Value : "",
                                    Body = HtmlRemoval.StripTagsRegex(el.Attribute("Body").Value),
                                };
                                yield return post;
                            }
                        }
                    }
                }
            }
        }
    }
}
