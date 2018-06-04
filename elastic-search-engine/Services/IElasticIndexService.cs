using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSerchEngine.Services
{
    public interface IElasticIndexService
    {
        void CreateIndex(string fileName, int maxItems);
    }
}
