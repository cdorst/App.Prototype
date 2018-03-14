using DevOps.Primitives.NuGet;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp6.Declarations.Helpers
{
    public static class NuGetDependencies
    {
        public static List<NuGetReference> Dependencies(params NuGetReference[] dependencies)
            => dependencies.ToList();

        public static NuGetReference NuGet(string name, string version)
            => new NuGetReference(name, version);
    }
}
