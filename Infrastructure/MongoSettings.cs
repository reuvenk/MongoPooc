using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoPocWebApplication1.Infrastructure
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string CollectionPrefix { get; set; }
    }
}
