public class WorkerThread extends Thread {
    public long sum = 0;
    public long iterations = 0;
    private final int id;
    private final double step;

    public WorkerThread(int id, double step) {
        this.id = id;
        this.step = step;
    }

    public String getWorkerThreadName() {
        return "Thread #" + this.id + " (step = " + this.step + ")";
    }

    public String getWorkerThreadResult() {
        return "Iterations = " + this.iterations + "; Sum = " + this.sum;
    }

    @Override
    public void run() {
        long start = System.currentTimeMillis();
        do {
            this.sum += this.step;
            this.iterations++;
        } while (!Thread.currentThread().isInterrupted());

        System.out.println(this.getWorkerThreadName() + ": " + this.getWorkerThreadResult() + "\n" + "\n------");
        System.out.println(System.currentTimeMillis() - start);
    }
}
