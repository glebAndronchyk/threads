package lb2.java;

public class ChunkWorker extends Thread {
    private final int _start;
    private final int _end;
    private final ComputableArray _instance;
    
    public ChunkWorker(int start, int end, ComputableArray instance) {
        this._instance = instance;
        this._start = start;
        this._end = end;
    }
    
    @Override
    public void run() {
        var min = this._instance.iterate(this._start, this._end);
        this._instance.collectMin(min[0], min[1]);
        this._instance.increaseCounter();
        this._instance.unlock();
    }
}
