
namespace lb1 {
    class WorkerThreadsController {
        private List<ThreadConfiguration> _threads;
        private bool _isFinished = false;

        public bool IsFinished
        {
            get { lock(this) { return this._isFinished; }}
            private set { lock(this) { this._isFinished = value; }}
        }

        public WorkerThreadsController(int threadsAmount) {
            this._threads = this.GenerateThreads(threadsAmount);
        }

        public void Run() {
            new Thread(() => {
                long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                this._threads.ForEach((entry) => {
                    entry.thread.Start();
                });

                while (!this.IsFinished) {
                    this.IsFinished = true;

                    this._threads.ForEach((entry) => {
                        if (entry.thread.IsAlive) {
                            this.IsFinished = false;

                            long elapsed = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - now;
                            if (elapsed >= entry.timeoutMs && !entry.thread.IsInterrupted) {
                                // System.Console.WriteLine(entry.timeoutMs);
                                entry.thread.Interrupt();
                            }
                        }
                    });
                }
            }).Start();
        }

        private List<ThreadConfiguration> GenerateThreads(int count) {
            var random = new Random();
            var result = new List<ThreadConfiguration>();
            int min = 1;
            int max = 10;

            for (int i = 0; i < count; i++)
            {
                Random rnd = new Random();
                int runDuration = rnd.Next(max - min + 1) + min;
                double step = (rnd.Next(max - min + 1) + min) * 10;
                result.Add(new ThreadConfiguration(new WorkerThread(i, step), runDuration * 1000));
            }

            return result;
        }
    }
}