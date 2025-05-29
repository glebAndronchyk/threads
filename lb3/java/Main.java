class Main {
    public static void main (String[] args) {
        var consumes = 3;
        var storageCapacity = 10;
        var totalOperators = 2;

        var storage = new Storage(storageCapacity);

        for (int i = 0; i < totalOperators; i++) {
            var consumer = new Consumer(consumes, storage, i);
            var producer = new Producer(consumes, storage, i);
                
            producer.start();
            consumer.start();
        }
    }
}