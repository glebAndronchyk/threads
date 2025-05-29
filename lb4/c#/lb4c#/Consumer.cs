namespace lb4;

public class Consumer
{
    private readonly GroupedResource _availableResource;
    private readonly Mediator _mediator;
    private readonly int _saturationLimit;
    private readonly Thread _consumingThread;

    private readonly Random _seed =  new Random();

    public Consumer(GroupedResource groupedResource, int saturationLimit, Mediator mediator)
    {
        this._mediator = mediator;
        this._availableResource = groupedResource;
        this._saturationLimit = saturationLimit;
        this._consumingThread = new Thread(this.Consume);
    }

    public void Start()
    {
        this._consumingThread.Start();
    }

    private void Consume()
    {
        Thread.CurrentThread.Name = $"{this._seed.GetHashCode()}";
        for (int i = 0; i < this._saturationLimit; i++)
        {
            this._mediator.RequestConsume(this._availableResource);
            this.EmulateAction();
            this._mediator.ReleaseConsume(this._availableResource);
        }
    }

    private void EmulateAction()
    {
        var ms = this._seed.Next(5000);
        System.Console.WriteLine($"{Thread.CurrentThread.Name}: starts consuming resource for {ms}ms");
        Thread.Sleep(ms);
    }
}
