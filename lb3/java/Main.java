class Main {
    public static void main (String[] args) {
        var consumes = 5;
        var storageCapacity = 20;
        var totalOperators = 1;

        var storage = new Storage(storageCapacity);

        for (int i = 0; i < totalOperators; i++) {
            var consumer = new Consumer(consumes, storage, i);
            var producer = new Producer(consumes, storage, i);
                
            producer.start();
            consumer.start();
        }
    }
}