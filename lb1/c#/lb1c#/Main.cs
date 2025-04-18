namespace lb1 {
    class Program {
        static void Main(string[] args)
        {
            int threadAmount = 20;
            new WorkerThreadsController(threadAmount).Run();
        }
    }
}