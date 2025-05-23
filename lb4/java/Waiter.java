import java.util.concurrent.Semaphore;

public class Waiter {
    private final Semaphore _consumingSemaphore;

    public Waiter(int totalConsumers) {
        var allowedConsumersAmount = totalConsumers - 1;
        this._consumingSemaphore = new Semaphore(allowedConsumersAmount);
    }

    public void requestPermissionToConsume() {
        try {
            this._consumingSemaphore.acquire();
        } catch (InterruptedException e) {
        }
    }

    public void releaseConsumePermission() {
        this._consumingSemaphore.release();
    }
}
