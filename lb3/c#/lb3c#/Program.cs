
namespace lb3;

class Program {
    public static void Main (String[] args) {
        var consumes = 3;
        var storageCapacity = 10;
        var totalOperators = 2;

        var storage = new Storage(storageCapacity);

        for (int i = 0; i < totalOperators; i++) {
            new Consumer(consumes, storage, i);
            new Producer(consumes, storage, i);    
        }
    }
}