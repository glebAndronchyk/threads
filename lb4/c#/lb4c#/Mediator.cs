using lb4;

public class Mediator
{
    private readonly Semaphore _consumingSemaphore;
    private readonly ResourceProvider _resourceProvider;

    public Mediator(ResourceProvider resourceProvider)
    {
        var totalConsumers = resourceProvider.TotalConsumers - 1;

        this._resourceProvider = resourceProvider;
        this._consumingSemaphore = new Semaphore(totalConsumers, totalConsumers);
    }

    public void RequestConsume(GroupedResource resourceGroup)
    {
        this._consumingSemaphore.WaitOne();
        this._resourceProvider.TakeConsumerRelatedResource(resourceGroup);
        System.Console.WriteLine($"{Thread.CurrentThread.Name}: took resource - {resourceGroup}");
    }

    public void ReleaseConsume(GroupedResource resourceGroup)
    {
        this._resourceProvider.FreeResourceGroup(resourceGroup);
        this._consumingSemaphore.Release();
        System.Console.WriteLine($"{Thread.CurrentThread.Name}: released resource - {resourceGroup}");
    }
}