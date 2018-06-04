using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSerchEngine.Services
{
    public interface IElasticConfig
    {
        IElasticClient GetClient();
    }
}
