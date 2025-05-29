using lb4;

var totalConsumers = 5;
var saturationLimit = 10;

var table = new ResourceProvider(totalConsumers);
var mediator = new Mediator(table);

for (int i = 0; i < totalConsumers; i++)
{
    var resourceGroupToConsume = table[i];
    var consumer = new Consumer(resourceGroupToConsume, saturationLimit, mediator);

    consumer.Start();
}
