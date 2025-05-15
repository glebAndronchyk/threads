public class Consumer extends Thread {
    private final Storage _store;
    private final int _consumes;
    private final int _id;

    public Consumer(int consumes, Storage store, int id) {
        this._store = store;
        this._consumes = consumes;
        this._id = id;
    }

    @Override
    public void run() {
        for (int i = 0; i < this._consumes; i++) {
            this._store.waitForRefill();
            ThreadSafeLogger.log("Consumer " + this._id + " waited for refill");

            this._store.enter();
            ThreadSafeLogger.log("Consumer " + this._id + " entered");

            var item = this._store.takeItem();
            ThreadSafeLogger.log("Consumer " + this._id + " took item: " + item);

            this._store.leave();
            ThreadSafeLogger.log("Consumer " + this._id + " left");
        }
    }
}
