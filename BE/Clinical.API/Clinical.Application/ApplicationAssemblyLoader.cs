using System.Reflection;

namespace Clinical.Application
{
    public static class ApplicationAssemblyLoader
    {
        public static void LoadAssembly(List<Assembly> assemblies)
        {
            var applicationAssembly = typeof(ApplicationAssemblyLoader).Assembly;
            if (assemblies == null) {
                assemblies = new List<Assembly>();
            }

            assemblies.Add(applicationAssembly);
        }
    }
}
