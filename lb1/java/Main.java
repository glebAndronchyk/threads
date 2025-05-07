class Main {
    public static void main (String[] args) {
        int threadsAmount = 5;

        System.out.println(ProcessHandle.current().pid());
        new WorkerThreadsController(threadsAmount);
    }
}