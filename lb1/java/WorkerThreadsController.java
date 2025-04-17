import java.util.List;

class WorkerThreadsController extends Thread {
    private List<ThreadConfiguration> _threads;
    private boolean _isFinished = false;

    public synchronized boolean isFinished() {
        return this._isFinished;
    }

    public WorkerThreadsController(List<ThreadConfiguration> threads) {
        this._threads = threads;
        this.run();
    }

    @Override
    public void run() {
        long now = System.currentTimeMillis();

        this._threads.forEach((entry) -> {
            entry.thread.start();
        });

        while (!this._isFinished) {
            this._isFinished = true;

            this._threads.forEach((entry) -> {
                if (entry.thread.isAlive()) {
                    this._isFinished = false;

                    long elapsed = System.currentTimeMillis() - now;
                    if (elapsed >= entry.timeoutMs) {
                        entry.thread.interrupt();
                    }
                }
            });
        }
    }
}
