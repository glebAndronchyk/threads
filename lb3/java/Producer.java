public class Producer extends Thread {
    private final Storage _store;
    private final int _consumes;
    private final int _id;

    public Producer(int consumes, Storage store, int id) {
        this._store = store;
        this._consumes = consumes;
        this._id = id;
    }

    @Override
    public void run() {
        for (int i = 0; i < this._consumes; i++) {
            this._store.waitForDiminution();
            ThreadSafeLogger.log("Producer " + this._id + " waited for diminution");

            this._store.enter();
            ThreadSafeLogger.log("Producer " + this._id + " entered");

            var item = this.getProductName("" + i);
            this._store.addItem(item);
            ThreadSafeLogger.log("Producer " + this._id + " added item: " + item);

            this._store.leave();
            ThreadSafeLogger.log("Producer " + this._id + " left");
        }
    }

    private String getProductName(String label) {
        return "producer-" + this._id + "-" + "product-" + label;
    }
}
