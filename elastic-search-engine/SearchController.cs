using ElasticSearchEngine.Services;
using ElasticSerchEngine.Models;
using ElasticSerchEngine.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ElasticSearchEngine
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ElasticSearchService _searchService;
        private readonly IElasticIndexService _indexService;

        public SearchController(IElasticIndexService elasticIndexService)
        {
            _indexService = elasticIndexService;
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
                return BadRequest($"Index Fail \r\n{ex}");
            }

            return Ok("Index Success");
        }

        [HttpGet]
        [Route("test")]
        public ActionResult Test()
        {
            return Ok("Test Success");
        }

        public IEnumerable<string> Get()
        {
            return new string[] { "Hey", "Universe!!!!" };
        }
    }
}
