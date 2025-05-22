import java.util.concurrent.Semaphore;

public class Table {
    private final Semaphore[] _forks;

    public Table(int totalSits) {
        var fair = true;
        var onePerUserPermit = 1;

        this._forks = new Semaphore[totalSits];
        for (int i = 0; i < this._forks.length; i++) {
            this._forks[i] = new Semaphore(onePerUserPermit, fair);
        }
    }

    public Sit getSit(int index) {
        var rightFork = index;
        var leftFork = (index + 1) % this._forks.length;

        return new Sit(leftFork, rightFork);
    }

    public void takeForks(Sit sit) {
        try {

            var isLastSit = sit.leftFork == this._forks.length - 1;

            if (isLastSit) {
                this._forks[sit.rightFork].acquire();
                this._forks[sit.leftFork].acquire();
            } else {
                this._forks[sit.leftFork].acquire();
                this._forks[sit.rightFork].acquire();
            }
        } catch (InterruptedException e) {
            throw new RuntimeException(e);
        }
    }

    public void putForks(Sit sit) {
        this._forks[sit.leftFork].release();
        this._forks[sit.rightFork].release();
    }
}
