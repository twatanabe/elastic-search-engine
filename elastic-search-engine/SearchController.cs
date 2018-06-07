using ElasticSearchEngine.Services;
using ElasticSerchEngine.Models;
using ElasticSerchEngine.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace ElasticSearchEngine
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IIndexService _indexService;
        private readonly ISearchService<Post> _searchService;
        private readonly ILogger _logger;

        public SearchController(IIndexService indexService, ISearchService<Post> searchService, ILogger<SearchController> logger)
        {
            _indexService = indexService;
            _searchService = searchService;
            _logger = logger;
        }

        // GET: api/search
        [HttpGet]
        [Route("search")]
        public ActionResult<SearchResult<Post>> Search(string query, int page = 1, int pageSize = 100)
        {
            SearchResult<Post> results = new SearchResult<Post>();
            try
            {
                results = _searchService.Search(query, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Search Fail \r\n{ex}");
                return BadRequest($"Index Fail \r\n{ex}");
            }

            _logger.LogInformation($"Search Success");
            return Ok(results);
        }

        [HttpGet]
        [Route("index")]
        public ActionResult Index(string fileName, int maxItesm = 1000)
        {
            try
            {
                _indexService.CreateIndex(fileName, maxItesm);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Index Fail \r\n{ex}");
                return BadRequest($"Index Fail \r\n{ex}");
            }

            _logger.LogInformation("Index Success");
            return Ok("Index Success");
        }

        [HttpGet]
        [Route("test")]
        public ActionResult Test()
        {
            _logger.LogTrace("tracing...");
            _logger.LogInformation("----- TEST SUCCESS -----");
            _logger.LogDebug("Debug");
            _logger.LogWarning("Waarrnnnnin yea!");
            _logger.LogError("!!!!ERROR!!!!");
            _logger.LogCritical("#critical");
            return Ok("Test Success");
        }

        public IEnumerable<string> Get()
        {
            return new string[] { "Hey", "Universe!!!!" };
        }
    }
}
