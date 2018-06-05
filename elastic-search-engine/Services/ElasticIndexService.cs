using ElasticSerchEngine.Config;
using ElasticSerchEngine.Models;
using ElasticSerchEngine.Util;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace ElasticSerchEngine.Services
{
    public class ElasticIndexService : IIndexService
    {
        private readonly IElasticClient client;
        private readonly IElasticConfig _elasticConfig;
        private readonly ILogger _logger;

        public ElasticIndexService(IElasticConfig elasticConfig, ILogger<ElasticIndexService> logger)
        {
            _elasticConfig = elasticConfig;
            client = _elasticConfig.GetClient();

            _logger = logger;
        }

        public bool IndexExists()
        {
            var response = client.IndexExists(_elasticConfig.IndexName).Exists;

            _logger.LogTrace($"Index Exists - {response}");

            return response;
        }

        public void CreateIndex(string fileName, int maxItems)
        {
            if (!IndexExists())
            {
                _logger.LogTrace("No Index Nope");
                var indexDescriptor = new CreateIndexDescriptor(_elasticConfig.IndexName)
                    .Mappings(ms => ms.Map<Post>(m => m.AutoMap()));

                var indexResponse = client.CreateIndex(_elasticConfig.IndexName, idx => indexDescriptor);
                _logger.LogTrace($"Create Index - {indexResponse.IsValid}");
            }

            var response = client.Bulk(b => b.Index<Post>(idx => idx.Document(new Post { })));

            _logger.LogTrace($"Bulk Index - {response.IsValid}");


            int i = 0;
            int take = maxItems;
            int batch = 1000;

            foreach (var batches in LoadPostsFromFile("Data/Posts.xml").Take(take).DoBatch(batch))
            {
                i++;
                var result = client.IndexMany<Post>(batches, _elasticConfig.IndexName);
            }
        }
        private IEnumerable<Post> LoadPostsFromFile(string inputUrl)
        {
            using (XmlReader reader = XmlReader.Create(inputUrl))
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
