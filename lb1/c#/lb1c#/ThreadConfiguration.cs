namespace lb1 {
    public class ThreadConfiguration {
        public WorkerThread thread;
        public long timeoutMs;

        public ThreadConfiguration(WorkerThread thread, long timeoutMs) {
            this.thread = thread;
            this.timeoutMs = timeoutMs;
        }
    }
}
