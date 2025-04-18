class Main {
    public static void main (String[] args) {
        int threadsAmount = 20;

        System.out.println(ProcessHandle.current().pid());
        new WorkerThreadsController(threadsAmount);
    }
}