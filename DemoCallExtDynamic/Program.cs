using System;
using ExtensionLib;

namespace DemoCallExtDynamic
{
    using System.Linq;
    using Util;
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Call Extension method normally ===");

            var utcNow = DateTime.UtcNow;

            var iso8601str = utcNow.ToIso8601String(true);

            Console.WriteLine($"UTC Now = {{{utcNow}}}\r\nISO8601 : {{{iso8601str}}}");

            Console.WriteLine("\r\n=== Call Extension method using reflection ===");

            var extensionLibAssembly = AssemblyUtil.TryLoadAssembly("ExtensionLib");
            if (extensionLibAssembly == null)
            {
                throw new Exception("Can not locate target Assembly!");
            }
            var targetExtMethod = extensionLibAssembly.GetExtensionMethods(typeof(string), "FromIso8601String").FirstOrDefault();

            var invokeResult = targetExtMethod.Invoke(null, new[] { iso8601str });

            if (invokeResult is DateTime convertBackDatetime)
            {
                Console.WriteLine($"Result is {{{convertBackDatetime}}}");
            }
        }
    }
}
