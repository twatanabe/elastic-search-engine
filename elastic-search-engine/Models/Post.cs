using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSerchEngine.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime? CreationDate { get; set; }

        [Text]
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<string> Suggest { get; set; }
    }
}
