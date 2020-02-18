using System;
using ExtensionLib;

namespace DemoCallExtDynamic
{
    using GranDen.CallExtMethodLib;
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Call Extension method normally ===");

            var utcNow = DateTime.UtcNow;

            var iso8601str = utcNow.ToIso8601String(true);

            Console.WriteLine($"UTC Now = {{{utcNow}}}\r\nISO8601 : {{{iso8601str}}}");

            Console.WriteLine("\r\n=== Call Extension method using reflection ===");

            var helper = new ExtMethodInvoker("ExtensionLib");
            var resultDateTime = helper.Invoke<DateTime>("FromIso8601String", iso8601str);
            Console.WriteLine($"Result is {{{resultDateTime}}}");
        }
    }
}
