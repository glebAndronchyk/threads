public class Philosopher extends Thread {
    private final Sit _sit;
    private final Table _table;

    public Philosopher(Sit sit, Table table) {
        this._sit = sit;
        this._table = table;
    }

    @Override
    public void run() {
        for (int i = 0; i < 10; i++) {
            System.out.println("Philosopher "  + this.getName() +  " is thinking " + (i + 1) + " times");
            this._table.takeForks(this._sit);
            System.out.println("Philosopher "  + this.getName() +  " is eating " + (i + 1) + " times");
            this.eat();
            this._table.putForks(this._sit);
        }
    }

    private void eat() {
        try {
            Thread.sleep((long) (Math.random() * 1000));
        } catch (InterruptedException e) {
        }
    }
}
