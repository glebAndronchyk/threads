namespace lb3;

class Consumer : Operator {
    public Consumer(int produces, Storage store, int id) : base(produces, store, id) {}

    protected override void Operate() {
        for (int i = 0; i < this._operations; i++) {
            this._store.WaitForRefill();
            System.Console.WriteLine("Consumer " + this._id + " waited for refill");

            this._store.Enter();
            System.Console.WriteLine("Consumer " + this._id + " entered");

            var item = this._store.TakeItem();
            System.Console.WriteLine("Consumer " + this._id + " took item: " + item);

            this._store.Leave();
            System.Console.WriteLine("Consumer " + this._id + " left");
        }
    }
}
