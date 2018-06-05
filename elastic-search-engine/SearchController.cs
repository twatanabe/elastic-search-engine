using ElasticSearchEngine.Services;
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
        private readonly ElasticSearchService _searchService;
        private readonly IIndexService _indexService;
        private readonly ILogger _logger;

        public SearchController(IIndexService elasticIndexService, ILogger<SearchController> logger)
        {
            _indexService = elasticIndexService;
            _logger = logger;
        }

        //// GET: api/search
        //[HttpGet]
        //[Route("search")]
        //public ActionResult<SearchResult<Post>> Search(string query, int page = 1, int pageSize = 10)
        //{
        //    //var results = service.Search(query, page, pageSize);
        //    //return Ok(results);
        //    return Ok();
        //}

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
