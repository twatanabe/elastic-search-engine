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
        public ActionResult<SearchResult<Post>> Search(string query = "", int page = 1, int pageSize = 100)
        {
            _logger.LogInformation("Search Called");

            if (query == "undefined")
            {
                query = "";
                _logger.LogTrace($"Undefined Search, return all");
            }

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
        [Route("searchbycategory")]
        public ActionResult<SearchResult<Post>> SearchByCategory([FromQuery]SearchInfo json)
        {
            string query = json.query;
            if (query == "undefined")
            {
                query = "";
            }
            var categories = (IEnumerable<string>)json.categories;
            return Ok(_searchService.SearchByCategory(query, categories, 1, 10));
        }

        [HttpGet]
        [Route("index")]
        public ActionResult Index(int maxItesm = 100000)
        {
            try
            {
                _indexService.CreateIndex(maxItesm);
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
        [Route("autocomplete")]
        public ActionResult Autocomplete(string q)
        {
            var x = _searchService.Autocomplete(q);
            return Ok(x);
        }

        [HttpGet]
        [Route("suggest")]
        public ActionResult Suggest(string q)
        {
            return Ok(_searchService.Suggest(q));
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

            _logger.LogInformation($"ElasticSearch - {_indexService.CanBeQueried()}");

            return Ok("Test Success");
        }

        [HttpGet]
        [Route("get")]
        public ActionResult<Post> Get(string id)
        {
            return Ok(_searchService.Get(id));
        }
    }
}
