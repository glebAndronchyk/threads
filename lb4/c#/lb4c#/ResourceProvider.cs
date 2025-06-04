namespace lb4;

public class ResourceProvider
{
    private readonly SemaphoreSlim[] _resources;

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
        this._resources = new SemaphoreSlim[totalConsumers];
        for (int i = 0; i < this._resources.Length; i++)
        {
            this._resources[i] = new SemaphoreSlim(onePerConsumerPermit, onePerConsumerPermit);
        }
    }

    public bool CheckAvailabilityOfConsumerRelatedResource(GroupedResource resourceGroup)
    {
        var onePerConsumerPermit = 1;
        var isLeftAvailable = this._resources[resourceGroup.LeftResource].CurrentCount == onePerConsumerPermit;
        var isRightAvailable = this._resources[resourceGroup.RightResource].CurrentCount == onePerConsumerPermit;

        return isLeftAvailable && isRightAvailable;
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

    // the problem is here

    public void FreeResourceGroup(GroupedResource resourceGroup)
    {
        this._resources[resourceGroup.LeftResource].Release();
        this._resources[resourceGroup.RightResource].Release();
    }
    
    private void AcquireLTR(GroupedResource resourceGroup) {
        this._resources[resourceGroup.LeftResource].Wait();
        this._resources[resourceGroup.RightResource].Wait();
    }


    private void AcquireRTL(GroupedResource resourceGroup) {
        this._resources[resourceGroup.RightResource].Wait();
        this._resources[resourceGroup.LeftResource].Wait();
    }
}