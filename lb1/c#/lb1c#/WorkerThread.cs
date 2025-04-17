namespace lb1 {
    public class WorkerThread {
        public double Sum { get; private set; } = 0;
        public int Iterations { get; private set; } = 0;
        private readonly int _id;
        private readonly double _step;
        private Thread _thread;
        private bool _isInterrupted = false;

        public bool IsAlive 
        {
            get { lock(this) { return this._thread.IsAlive; }}
        }

        public bool IsInterrupted 
        {
            get { lock(this) { return this._isInterrupted; }}
            set { lock(this) { this._isInterrupted = value; }}
        }

        public WorkerThread(int id, double step) {
            this._id = id;
            this._step = step;
            this._thread = new Thread(this.Run);
        }

        public void Interrupt() {
            this.IsInterrupted = true;
        }

        public void Start() {
            this._thread.Start();
        }

        public string GetWorkerThreadName()
        {
            return $"Thread #{_id} (step = {_step})";
        }

        public string GetWorkerThreadResult()
        {
            return $"Iterations = {Iterations}; Sum = {Sum}";
        }

        private void Run() {
            do {
                this.Sum += this._step;
                this.Iterations++;
            } while (!this._isInterrupted);

            Console.WriteLine($"{GetWorkerThreadName()}: {GetWorkerThreadResult()}\n------");
            this._thread.Interrupt();
        }
    }
}