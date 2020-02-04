using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DemoCallExtDynamic.Util
{
    static class AssemblyUtil
    {
        public static Assembly TryLoadAssembly(string name)
        {
            var loadedAssembly =
                AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(c => c.FullName.Contains(name));

            if (loadedAssembly != null) { return loadedAssembly; }

            var assemblyName = Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                .FirstOrDefault(c => c.FullName.Contains(name));

            return assemblyName == null ? null : Assembly.Load(assemblyName);
        }

        public static IEnumerable<MethodInfo> GetExtensionMethods(this Assembly assembly, Type extendedType, string extensionMethodName)
        {
            var extMethods =
                from type in assembly.GetTypes()
                where type.IsSealed && !type.IsGenericType && !type.IsNested
                from method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                where method.IsDefined(typeof(ExtensionAttribute), false)
                where method.Name == extensionMethodName
                where method.GetParameters()[0].ParameterType == extendedType
                select method;

            return extMethods;
        }
    }
}
