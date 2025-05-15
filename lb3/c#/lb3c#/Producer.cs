namespace lb3;

class Producer : Operator {
    public Producer(int produces, Storage store, int id) : base(produces, store, id) {}

    protected override void Operate() {
        for (int i = 0; i < this._operations; i++) {
            this._store.WaitForDiminution();
            System.Console.WriteLine("Producer " + this._id + " waited for diminution");
            
            this._store.Enter();
            System.Console.WriteLine("Producer " + this._id + " entered");
            
            var item = this.GetProductName($"{i}");
            this._store.AddItem(item);
            System.Console.WriteLine("Producer " + this._id + " added item: " + item);

            this._store.Leave();
            System.Console.WriteLine("Producer " + this._id + " left");
        }
    }

    private string GetProductName(string label) {
        return $"Producer-${this._id}-product-{label}";
    }
}
