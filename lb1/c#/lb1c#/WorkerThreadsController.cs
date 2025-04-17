
namespace lb1 {
    class WorkerThreadsController {
        private List<ThreadConfiguration> _threads;
        private bool _isFinished = false;

        public bool IsFinished
        {
            get { lock(this) { return this._isFinished; }}
            set { lock(this) { this._isFinished = value; }}
        }

        public WorkerThreadsController(List<ThreadConfiguration> threads) {
            this._threads = threads;
        }

        public void Run() {
            new Thread(() => {
                long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                this._threads.ForEach((entry) => {
                    entry.thread.Start();
                });

                while (!this._isFinished) {
                    this._isFinished = true;

                    this._threads.ForEach((entry) => {
                        if (entry.thread.IsAlive) {
                            this._isFinished = false;

                            long elapsed = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - now;
                            if (elapsed >= entry.timeoutMs) {
                                entry.thread.Interrupt();
                            }
                        }
                    });
                }
            }).Start();
        }
    }
}