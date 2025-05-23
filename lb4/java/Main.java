class Main {
    public static void main (String[] args) {
        var sits = 10;
        Waiter waiter = new Waiter(sits);
        Table table = new Table(sits, waiter);

        for (int i = 0; i < sits; i++) {
            new Philosopher(table.getSit(i), table).start();
        }
    }
}