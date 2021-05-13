using System.IO;
using Attest.Testing.Contracts;

namespace MongoPoc.Specs.Tests.Modules
{
    internal sealed class ApplicationPathInfo : IApplicationPathInfo
    {
        public string Executable => "MongoPocWebApplication1.csproj";
        public string RelativePath => Path.Combine("..", "..", "..", "..", "MongoPocWebApplication1");
    }
}