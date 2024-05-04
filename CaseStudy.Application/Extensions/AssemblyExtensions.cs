using System.Reflection;

namespace CaseStudy.Application.Extensions;

internal static class AssemblyExtensions
{
    internal static bool Contains(this Assembly assembly, params Type[] types)
    {
        var assemblyTypes = assembly.GetTypes().SelectMany(t => new[] { t }.Concat(t.GetNestedTypes()));
        return types.Any(type => assemblyTypes.Contains(type));
    }

    internal static IEnumerable<Assembly> AppDomainContains(params Type[] types)
    {
        return AppDomain.CurrentDomain.GetAssemblies().Where(a => a.Contains(types));
    }
    internal static IEnumerable<Assembly> FindAssembliesWithImplementationsOf(this IEnumerable<Assembly> assemblies, Type targetType)
    {
        var resultAssemblies = new HashSet<Assembly>();

        foreach (var assembly in assemblies)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsClass || type.IsAbstract || type == targetType)
                {
                    continue;
                }

                bool typeMatches;

                if (targetType.IsGenericTypeDefinition)
                {
                    typeMatches = type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == targetType) ||
                                  type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == targetType;
                }
                else
                {
                    typeMatches = targetType.IsAssignableFrom(type);
                }

                if (!typeMatches) continue;

                resultAssemblies.Add(assembly);
                break;
            }
        }

        return resultAssemblies;
    }
    internal static IEnumerable<Assembly> GetAllAppDomainAssemblies()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

        var assemblyDir = AppDomain.CurrentDomain.BaseDirectory;

        var allDllFiles = Directory.GetFiles(assemblyDir, "*.dll", SearchOption.AllDirectories);

        foreach (var dllFile in allDllFiles)
        {
            try
            {
                var assemblyName = AssemblyName.GetAssemblyName(dllFile);

                if (assemblies.Any(a => a.FullName == assemblyName.FullName)) continue;

                var loadedAssembly = Assembly.Load(assemblyName);
                assemblies.Add(loadedAssembly);
            }
            catch
            {
                // ignored
            }
        }

        return assemblies;
    }
}