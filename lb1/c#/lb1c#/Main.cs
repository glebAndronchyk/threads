namespace lb1 {
    class Program {
        static void Main(string[] args)
        {
            List<ThreadConfiguration> threads = new List<ThreadConfiguration>()
            {
                new ThreadConfiguration(new WorkerThread(13, 2), 2000),
                new ThreadConfiguration(new WorkerThread(14, 1000), 5000),
                new ThreadConfiguration(new WorkerThread(16, 4), 4000),
                new ThreadConfiguration(new WorkerThread(10, 5), 10000),
            };

            new WorkerThreadsController(threads).Run();
        }
    }
}