import java.time.Duration;
import java.time.Instant;

import lb2.java.ComputableArray;

class Main {
    public static void main (String[] args) {
        var ca = new ComputableArray(1000000000, 10);

        System.out.println("\n-----\n");

        long startSyncTime = System.currentTimeMillis();
        var minSync = ca.minSync();
        long endSyncTime = System.currentTimeMillis();
        long syncElapsedTime = endSyncTime - startSyncTime;

        System.out.println("Sync result:" + "\n" +  "value:" + minSync[0] + "\n" + "index:" + minSync[1] + "\n" + syncElapsedTime + "ms");

        System.out.println("\n-----\n");

        long startAsyncTime = System.currentTimeMillis();
        var minAsync = ca.minAsync();
        long endAsyncTime = System.currentTimeMillis();
        long asyncElapsedTime = endAsyncTime - startAsyncTime;

        System.out.println("Async result:" + "\n" + "value:" + minAsync[0] + "\n" + "index:" + minAsync[1] + "\n" + asyncElapsedTime + "ms");
        System.out.println("\n-----\n");
        
    }
}