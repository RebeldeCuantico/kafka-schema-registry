using Common;
using Confluent.Kafka;
using JsonFormat;
using static Confluent.Kafka.ConfigPropertyNames;

var bootstrapServers = "localhost:9092";
string schemaRegistryUrl = "http://localhost:8081";
string topicName = "json-topic";
string consumerGroup = "json-cg-001";

var cts = new CancellationTokenSource();
var vehicleProducer = new JsonProducer<Vehicle>(bootstrapServers, schemaRegistryUrl, topicName);
var vehicleConsumer = new JsonConsumer<Vehicle>(bootstrapServers, schemaRegistryUrl, consumerGroup, topicName);

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
            Coordinates = Generator.GetCoordinates(),
            Registration = Generator.GetRegistration(),
            Speed = Generator.GetSpeed()
        };

        Console.WriteLine($"Sending: Vehicle {vehicle.Registration} is in {vehicle.Coordinates} with a speed of {vehicle.Speed} km/h at {DateTime.Now}. Ctrl+C to exit.");

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
             Console.WriteLine($"Receiving: Vehicle {vehicle.Registration} is in {vehicle.Coordinates} with a speed of {vehicle.Speed} km/h at {DateTime.Now} ");
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



//var consumerConfig = new ConsumerConfig
//{
//    BootstrapServers = bootstrapServers,
//    GroupId = "json-cg"
//};

//var jsonSerializerConfig = new JsonSerializerConfig
//{
//    BufferBytes = 100
//};

