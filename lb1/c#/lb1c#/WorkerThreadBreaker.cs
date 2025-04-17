namespace lb1 {
    class WorkerThreadBreaker {
        private readonly int _msTimeout = 0;
        private bool _canStop = false;
        private Thread _thread = null;

        public WorkerThreadBreaker(int msTimeout)
        {
            _msTimeout = msTimeout;
            _thread = new Thread(Timeout);
            _thread.Start();
        }

        private void Timeout() {
            try 
            {
                Thread.Sleep(this._msTimeout);
            } 
            catch (ThreadInterruptedException error)
            {
                Console.WriteLine(error.ToString());
            }
            finally
            {
                this._canStop = true;
            }
        }

        public string GetMeta()
        {
            return $"Timeout = {_msTimeout}";
        }

        public bool IsCanStop
        {
            get { lock(this) { return _canStop; } }
        }
    }
}