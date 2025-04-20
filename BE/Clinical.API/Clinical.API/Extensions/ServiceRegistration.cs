using System.Reflection;
using Clinical.Application;

namespace Clinical.Api.Extensions
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            var assamblies = LoadAssemblies();
            services.RegisterAppServices(assamblies);
        }
        private static IList<Assembly> LoadAssemblies()
        {
            var assemblies = new List<Assembly>(0);
            ApplicationAssemblyLoader.LoadAssembly(assemblies);
            return assemblies;
        }
        private static void RegisterAppServices(this IServiceCollection services, IList<Assembly> assemblies)
        {
            var serviceTypesFromAssemblies = assemblies
                .SelectMany(a => a.DefinedTypes
                    .Where(t => t.IsClass && t.Name.EndsWith("Service")))
                .ToList();
            foreach (var typeInfo in serviceTypesFromAssemblies)
            {
                Attribute? existAttribute = Attribute.GetCustomAttribute(typeInfo, typeof(ObsoleteAttribute));
                if (existAttribute != null)
                {
                    continue;
                }

                var interfaces = typeInfo.GetInterfaces().Where(t => !t.IsGenericType).ToList();
                foreach (var iface in interfaces)
                {
                    services.Add(new ServiceDescriptor(iface, typeInfo, ServiceLifetime.Scoped));
                }
            }
        }
    }

}