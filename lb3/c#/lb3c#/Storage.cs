using System.Threading;
using System.Collections.Generic;

namespace lb3;

class Storage {
    private readonly int _usersRatePermit = 1;
    private readonly Stack<string> _internalStorage = new ();
    private readonly int _capacity;

    private readonly Semaphore _accessSemaphore;
    private readonly Semaphore _storageCapacitySemaphore;
    private readonly Semaphore _storageEmptySemaphore;

    public Storage(int capacity) {
        var zeroPermits = 0;

        this._capacity = capacity;
        this._accessSemaphore = new Semaphore(this._usersRatePermit, _usersRatePermit);
        this._storageCapacitySemaphore = new Semaphore(this._capacity, this._capacity);
        this._storageEmptySemaphore = new Semaphore(zeroPermits, this._capacity);
    }

    public void WaitForDiminution() {
        this._storageCapacitySemaphore.WaitOne();
    }

    public void WaitForRefill() {
        this._storageEmptySemaphore.WaitOne();
    }

    public void Enter() {
        this._accessSemaphore.WaitOne();   
    }

    public void Leave() {
        this._accessSemaphore.Release();
    }

    public string AddItem(string item) {
        this._internalStorage.Push(item);

        this._storageEmptySemaphore.Release();
        return item;
    }

    public string TakeItem() {
        var item = this._internalStorage.Pop();

        this._storageCapacitySemaphore.Release();
        return item;
    }
}
