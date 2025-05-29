namespace lb4;

public class ResourceProvider
{
    private readonly Semaphore[] _resources;

    public GroupedResource this[int index]
    {
        get
        {
            var rightFork = index;
            var leftFork = (index + 1) % this._resources.Length;

            return new GroupedResource(leftFork, rightFork);
        }
    }

    public int TotalConsumers
    {
        get
        {
            return this._resources.Length;
        }
    }

    public ResourceProvider(int totalConsumers)
    {
        var onePerConsumerPermit = 1;
        this._resources = new Semaphore[totalConsumers];
        for (int i = 0; i < this._resources.Length; i++)
        {
            this._resources[i] = new Semaphore(onePerConsumerPermit, onePerConsumerPermit);
        }
    }

    public void TakeConsumerRelatedResource(GroupedResource resourceGroup)
    {
        var isLastSit = resourceGroup.IsLastInQueue(this._resources.Length - 1);

        if (isLastSit)
        {
            AcquireRTL(resourceGroup);
        }
        else
        {
            AcquireLTR(resourceGroup);
        }
    }

    public void FreeResourceGroup(GroupedResource resourceGroup)
    {
        this._resources[resourceGroup.LeftResource].Release();
        this._resources[resourceGroup.RightResource].Release();
    }
    
    private void AcquireRTL(GroupedResource resourceGroup) {
        this._resources[resourceGroup.LeftResource].WaitOne();
        this._resources[resourceGroup.RightResource].WaitOne();
    }


    private void AcquireLTR(GroupedResource resourceGroup) {
        this._resources[resourceGroup.RightResource].WaitOne();
        this._resources[resourceGroup.LeftResource].WaitOne();
    }
}