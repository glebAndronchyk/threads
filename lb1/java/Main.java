import java.util.Arrays;
import java.util.List;

class Main {
    public static void main (String[] args) {
        List<ThreadConfiguration> workerPool = Arrays.asList(
            new ThreadConfiguration() {{ thread = new WorkerThread(13, 2); timeoutMs = 40000; }},
            new ThreadConfiguration() {{ thread = new WorkerThread(12, 2); timeoutMs = 2000; }},
            new ThreadConfiguration() {{ thread = new WorkerThread(14, 2); timeoutMs = 2000; }},
            new ThreadConfiguration() {{ thread = new WorkerThread(15, 2); timeoutMs = 2000; }},
            new ThreadConfiguration() {{ thread = new WorkerThread(16, 2); timeoutMs = 2000; }},
            new ThreadConfiguration() {{ thread = new WorkerThread(18, 2); timeoutMs = 2000; }},
            new ThreadConfiguration() {{ thread = new WorkerThread(11, 2); timeoutMs = 2000; }},
            new ThreadConfiguration() {{ thread = new WorkerThread(1221, 2); timeoutMs = 2000; }}
        );

        new WorkerThreadsController(workerPool);
    }
}