import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class ThreadSafeLogger {
    private static final Lock lock = new ReentrantLock();
    private static final DateTimeFormatter formatter = DateTimeFormatter.ofPattern("HH:mm:ss.SSSSSS");
    
    public static void log(String message) {
        lock.lock();
        try {
            String timestamp =  LocalDateTime.now().format(formatter);
            System.out.println("[" + timestamp + "] " + message);
        } finally {
            lock.unlock();
        }
    }
}