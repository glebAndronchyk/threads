public class ThreadConfiguration {
    public Thread thread;
    public long timeoutMs;

    public ThreadConfiguration(Thread thread, long timeout) {
        this.thread = thread;
        this.timeoutMs = timeout;
    }
}
