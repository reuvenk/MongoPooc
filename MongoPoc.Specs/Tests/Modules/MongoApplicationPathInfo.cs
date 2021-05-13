using System.IO;
using Attest.Testing.Contracts;

namespace MongoPoc.Specs.Tests.Modules
{
    internal sealed class MongoApplicationPathInfo : IApplicationPathInfo
    {
        public string Executable => "docker-compose.yml";
        public string RelativePath => Path.Combine("..", "..", "..", "..");
    }
}