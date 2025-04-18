namespace lb1 {
    class Program {
        static void Main(string[] args)
        {
            int threadAmount = 1;
            new WorkerThreadsController(threadAmount).Run();
        }
    }
}