import java.util.Optional;
import java.util.concurrent.Semaphore;

public class Table {
    private final Semaphore[] _forks;
    private final Optional<Waiter> _waiter;

    public Table(int totalSits, Optional<Waiter> waiter) {
        var fair = true;
        var onePerUserPermit = 1;

        this._forks = new Semaphore[totalSits];
        this._waiter = waiter;
        for (int i = 0; i < this._forks.length; i++) {
            this._forks[i] = new Semaphore(onePerUserPermit, fair);
        }
    }

    public Table(int totalSits) {
        this(totalSits, Optional.empty());
    }

    public Table(int totalSits, Waiter waiter) {
        this(totalSits, Optional.of(waiter));
    }

    public Sit getSit(int index) {
        var rightFork = index;
        var leftFork = (index + 1) % this._forks.length;

        return new Sit(leftFork, rightFork);
    }

    public void takeForks(Sit sit) {
        if (this._waiter.isPresent()) {
            this._waiter.get().requestPermissionToConsume();
        }
        
        var isLastSit = sit.isLastIn(this._forks.length - 1);

        if (isLastSit) {
            acquireRTL(sit);
        } else {
            acquireLTR(sit);
        }
    }

    public void putForks(Sit sit) {
        if (this._waiter.isPresent()) {
            this._waiter.get().releaseConsumePermission();
        }

        this._forks[sit.leftFork].release();
        this._forks[sit.rightFork].release();
    }

    private void acquireRTL(Sit sit) {
        try {
            this._forks[sit.rightFork].acquire();
            this._forks[sit.leftFork].acquire();
        } catch (InterruptedException e) {
            throw new RuntimeException(e);
        }

    }


    private void acquireLTR(Sit sit) {
        try {
            this._forks[sit.leftFork].acquire();
            this._forks[sit.rightFork].acquire();
        } catch (InterruptedException e) {
            throw new RuntimeException(e);
        }
    }
}
