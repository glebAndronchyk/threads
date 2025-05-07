package lb2.java;

import java.util.Random;

public class ComputableArray {
    public final int[] arr; 

    private Integer _min = null;
    private Integer _minIndex = null;
    private final int _totalThreads;
    private int _finishedThreads = 0;

    public ComputableArray(int length, int threadsAmount) {
        this.arr = new int[length];
        this._totalThreads = threadsAmount;
        this.fillArray(100, 200);
    }

    public int[] minSync() {
        this._min = null;
        return this.iterate(0, this.arr.length);
    }

    public int[] minAsync() {
        this._min = null;
        this._findLowestValue();
        return new int[] { this._min, this._minIndex };
    }

    public synchronized void increaseCounter() {
        this._finishedThreads++;
    }

    public synchronized void unlock() {
        notify();
    }

    public synchronized void collectMin(int val, int index) {
        if (this._min == null || this._min > val) {
            this._min = val;
            this._minIndex = index;
        }
    }

    public int[] iterate(int start, int end) {
        int min = Integer.MAX_VALUE;
        int minIndex = 0;
        for (int i = start; i < end; i++) {
            int entry = this.arr[i];
            if (min > entry) {
                min = entry;
                minIndex = i;
            }
        }

        return new int[] { min, minIndex };
    }

    private void _findLowestValue() {
        int chunkSize = this.arr.length / this._totalThreads;
        int remainder = this.arr.length % this._totalThreads;

        for (int i = 0; i < this._totalThreads; i++) {
            int chunkOffsetSize = chunkSize + (i < remainder ? 1 : 0);
            int start = i * chunkOffsetSize;
            int end = start + chunkOffsetSize;
            var cw = new ChunkWorker(start, end, this);
            cw.start();
        }

        stutterComputations();
    }

    private synchronized void stutterComputations() {
        while(this._finishedThreads < this._totalThreads) {
            try {
                wait();
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }
        }

        this._finishedThreads = 0;
    }

    private void fillArray(int min, int max) {
        var rnd = new Random();

        for (int i = 0; i < this.arr.length; i++) {
            this.arr[i] = rnd.nextInt(max - min + 1) + min;
        }

        int negativeIdx = rnd.nextInt(0, this.arr.length);
        this.arr[negativeIdx] = -this.arr[negativeIdx];
    }
}
