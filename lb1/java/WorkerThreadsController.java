import java.util.ArrayList;
import java.util.List;
import java.util.Random;

class WorkerThreadsController extends Thread {
    private List<ThreadConfiguration> _threads;
    private boolean _isFinished = false;

    public synchronized boolean isFinished() {
        return this._isFinished;
    }

    private synchronized void setIsFinished(boolean value) {
        this._isFinished = value;
    }

    public WorkerThreadsController(int threadsAmount) {
        this._threads = this.generateThreads(threadsAmount);
        this.run();
    }

    @Override
    public void run() {
        long now = System.currentTimeMillis();

        this._threads.forEach((entry) -> {
            entry.thread.start();
        });

        while (!this._isFinished) {
            this.setIsFinished(true);

            this._threads.forEach((entry) -> {
                if (entry.thread.isAlive()) {
                    this.setIsFinished(false);

                    if (!entry.thread.isInterrupted()) {
                        long elapsed = System.currentTimeMillis() - now;
                        if (elapsed >= entry.timeoutMs) {
                            entry.thread.interrupt();
                        }
                    }
                }
            });
        }
    }

    private List<ThreadConfiguration> generateThreads(int count) {
        Random random = new Random();
        List<ThreadConfiguration> result = new ArrayList<ThreadConfiguration>();
        int min = 20;
        int max = 50;

        for (int i = 0; i < count; i++) {
            int runDuration = random.nextInt(max - min + 1) + min;
            double step = (random.nextInt(max - min + 1) + min) * 10.0;
            result.add(new ThreadConfiguration(new WorkerThread(i, step), runDuration * 1000));
        }

        return result;
    }
}
