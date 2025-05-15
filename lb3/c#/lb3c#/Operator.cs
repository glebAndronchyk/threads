namespace lb3;

abstract class Operator {
    private readonly Thread _thread;
    protected readonly Storage _store;
    protected readonly int _id;
    protected readonly int _operations;


    protected Operator(int operations, Storage store, int id) {
        this._store = store;
        this._operations = operations;
        this._id = id;

        this._thread = new Thread(this.Operate);
        this._thread.Start();
    }

    protected abstract void Operate();
}