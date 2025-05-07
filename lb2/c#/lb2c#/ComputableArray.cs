namespace lb2 {
    class ComputableArray {
        private readonly int[] _arr;
        private readonly int _totalThreads;
        private readonly object _minValueLocker = new object();
        private readonly object _threadCountLocker = new object();

        private int _usedThreads = 0;
        private int _asyncMin;
        private int _minIndex;

        public int[] GetMinAsync() {
            int chunkSize = this._arr.Length / this._totalThreads;
            int remainder = this._arr.Length % this._totalThreads;

            for (int i = 0; i < this._totalThreads; i++) {
                int chunkOffsetSize = chunkSize + (i < remainder ? 1 : 0);
                int start = i * chunkOffsetSize;
                int end = start + chunkOffsetSize;
                var thread = new Thread(this._iterateAsync);
                thread.Start(new IterationBounds(){
                    start = start,
                    end = end,
                    cb = (int val, int currentMin) => val < currentMin ? val : null
                });
            }

            _stutterComputations();

            return new int[] { this._asyncMin, this._minIndex };
        }

        public int[] GetMinSync() {
            return _iterate(0, this._arr.Length, (x, currentMin) => {
                if (x < currentMin) {
                    return x;
                }

                return null;
            });
        }

        public ComputableArray(int length, int threadsAmount) {
            this._arr = new int[length];
            this._totalThreads = threadsAmount;
            this._fillArray(100, 200);
        }

        private void _fillArray(int min, int max) {
            var rnd = new Random();

            for (int i = 0; i < this._arr.Length; i++) {
                this._arr[i] = rnd.Next(min, max + 1);
            }

            int negativeIdx = rnd.Next(0, this._arr.Length);
            this._arr[negativeIdx] = -this._arr[negativeIdx];
        }

        private void _iterateAsync(object iterationBounds) {
            var ib = (iterationBounds as IterationBounds);
            if (ib != null) {
                int result = int.MaxValue;
                int index = -1;
                for (int i = ib.start; i < ib.end; i++) {
                    var cbResult = ib.cb(this._arr[i], result);
                    if (cbResult != null) {
                        index = i;
                        result = (int)cbResult;
                    }
                }

                lock(this._minValueLocker) {
                    this._collectMin(result, index);
                }

                lock(this._threadCountLocker) {
                    this._incrementUsedThreadsCount();
                }
            }
        }

        private int[] _iterate(int start, int end, Func<int, int, int?> cb) {
            int result = Int32.MaxValue;
            int index = -1;
            for (int i = start; i < end; i++) {
                var cbResult = cb(this._arr[i], result);
                if (cbResult != null) {
                    result = (int)cbResult;
                    index = i;
                }
            }

            return new int[] { result, index };
        }

        private void _stutterComputations() {
            lock(this._threadCountLocker) {
                while (this._totalThreads > this._usedThreads) {
                    Monitor.Wait(this._threadCountLocker);
                }
            }
        }

        private void _collectMin(int minValue, int index) {
            if (this._asyncMin > minValue) {
                this._asyncMin = minValue;
                this._minIndex = index;
            }
        }

        private void _incrementUsedThreadsCount() {
            this._usedThreads++;
            Monitor.PulseAll(this._threadCountLocker);
        }
    }
}

