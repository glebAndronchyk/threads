public class Sit {
    public int leftFork;
    public int rightFork;

    public Sit(int leftFork, int rightFork) {
        this.leftFork = leftFork;
        this.rightFork = rightFork;
    }

    public boolean isLastIn(int total) {
        return  this.leftFork == total;
    }
}
