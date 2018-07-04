using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSerchEngine.Models
{
    public class SearchInfo
    {
        public string query { get; set; }
        public List<string> categories { get; set; }
    }
}
