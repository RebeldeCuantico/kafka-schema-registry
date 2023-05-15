using AvroConsole;
using AvroConsole.Entity;
using Common;
using Confluent.Kafka;


var bootstrapServers = "localhost:9092";
string schemaRegistryUrl = "http://localhost:8081";
string topicName = "avro-topic";
string consumerGroup = "avro-cg-001";

var cts = new CancellationTokenSource();
var vehicleProducer = new AvroProducer<Vehicle>(bootstrapServers, schemaRegistryUrl, topicName);
var vehicleConsumer = new AvroConsumer<Vehicle>(bootstrapServers, schemaRegistryUrl, consumerGroup, topicName);

Console.CancelKeyPress += (sender, e) =>
{
    vehicleProducer.Close();
    cts.Cancel();
    e.Cancel = true;
};

var produce = Task.Run(async () =>
{
    vehicleProducer.Build();

    while (true)
    {
        var vehicle = new Vehicle
        {
            coordinates = Generator.GetCoordinates(),
            registration = Generator.GetRegistration(),
            speed = Generator.GetSpeed()
        };

        Console.WriteLine($"Sending: Vehicle {vehicle.registration} is in {vehicle.coordinates} with a speed of {vehicle.speed} km/h at {DateTime.Now}. Ctrl+C to exit.");

        await vehicleProducer.ProduceAsync(vehicle);

        Thread.Sleep(2000);
    }
});

var consumer = Task.Run(() =>
{
    vehicleConsumer.Build();

    while (true)
    {
        try
        {
            var vehicle = vehicleConsumer.Consume(cts.Token);
            Console.WriteLine($"Receiving: Vehicle {vehicle.registration} is in {vehicle.coordinates} with a speed of {vehicle.speed} km/h at {DateTime.Now} ");
        }
        catch (ConsumeException e)
        {
            Console.WriteLine($"Error ---> {e.Error.Reason}");
        }
    }
});

Console.WriteLine("The application is running. Press Ctrl+C to exit.");
while (true)
{
    Thread.Sleep(1000);
}
