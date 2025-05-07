using System.Diagnostics;

namespace lb1 {
    class Program {
        static void Main(string[] args)
        {
            int threadAmount = 20;

            System.Console.WriteLine(Process.GetCurrentProcess().Id);
            new WorkerThreadsController(threadAmount).Run();
        }
    }
}