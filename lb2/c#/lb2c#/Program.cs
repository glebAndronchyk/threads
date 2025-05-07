using System.Diagnostics;

namespace lb2 {
    class Program {
        static void Main(string[] args)
        {
            var ca = new ComputableArray(1000000000, 10);
            var sw = new Stopwatch();

            sw.Start();
            var syncResult = ca.GetMinSync();
            Console.WriteLine($"\n----\nSync result:\nvalue:{syncResult[0]}\nindex:{syncResult[1]}");
            sw.Stop();
            Console.WriteLine($"{sw.ElapsedMilliseconds}ms");

            sw.Restart();
            var asyncResult = ca.GetMinAsync();
            Console.WriteLine($"\n----\nAsync result:\nvalue:{asyncResult[0]}\nindex:{asyncResult[1]}");
            sw.Stop();
            Console.WriteLine($"{sw.ElapsedMilliseconds}ms");
        }
    }
}
