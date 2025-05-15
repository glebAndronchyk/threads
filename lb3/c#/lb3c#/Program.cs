
namespace lb3;

class Program {
    public static void Main (String[] args) {
        var consumes = 5;
        var storageCapacity = 20;
        var totalOperators = 10;

        var storage = new Storage(storageCapacity);

        for (int i = 0; i < totalOperators; i++) {
            new Consumer(consumes, storage, i);
            new Producer(consumes, storage, i);    
        }
    }
}