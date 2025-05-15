import java.util.Stack;
import java.util.concurrent.Semaphore;

public class Storage {
    private final int _usersRatePermit = 1;
    private final Stack<String> _internalStorage = new Stack<>();
    private final int _capacity;

    private final Semaphore _accessSemaphore;
    private final Semaphore _storageCapacitySemaphore;
    private final Semaphore _storageEmptySemaphore;

    public Storage(int capacity) {
        var fair = true;
        var zeroPermits = 0;

        this._capacity = capacity;
        this._accessSemaphore = new Semaphore(this._usersRatePermit);
        this._storageCapacitySemaphore = new Semaphore(this._capacity);
        this._storageEmptySemaphore = new Semaphore(zeroPermits);
    }

    public void waitForDiminution() {
        try {
            this._storageCapacitySemaphore.acquire();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    public void waitForRefill() {
        try {
            this._storageEmptySemaphore.acquire();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    public void enter() {
        try {
           this._accessSemaphore.acquire();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    public String addItem(String item) {
        this._internalStorage.push(item);

        this._storageEmptySemaphore.release();
        return item;
    }

    public String takeItem() {
        var item = this._internalStorage.pop();

        this._storageCapacitySemaphore.release();
        return item;
    }

    public void leave() {
        this._accessSemaphore.release();
    }
}
