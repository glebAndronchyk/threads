class Main {
    public static void main (String[] args) {
        var sits = 10;
        Table table = new Table(sits);

        for (int i = 0; i < sits; i++) {
            new Philosopher(table.getSit(i), table).start();
        }
    }
}