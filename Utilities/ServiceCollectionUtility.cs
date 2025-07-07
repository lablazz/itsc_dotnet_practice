using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace itsc_dotnet_practice.Utilities
{
    public static class ServiceCollectionUtility
    {
        public static void AddScopedServicesByConvention(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes();

            var interfaces = types.Where(t => t.IsInterface && t.Name.StartsWith("I")).ToList();

            foreach (var interfaceType in interfaces)
            {
                var implementationType = types.FirstOrDefault(t =>
                    t.IsClass &&
                    !t.IsAbstract &&
                    t.Name == interfaceType.Name.Substring(1) &&
                    interfaceType.IsAssignableFrom(t));

                if (implementationType != null)
                {
                    Console.WriteLine($"Registering: {interfaceType.Name} -> {implementationType.Name}");
                    services.AddScoped(interfaceType, implementationType);
                }
                else
                {
                    Console.WriteLine($"Skipping: {interfaceType.Name}, no implementation found.");
                }
            }
        }
    }
}
