using Common;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry.Serdes;
using Google.Protobuf;

namespace Protobuf
{
    public class ProtoConsumer<T> : ConsumerBase<T>
        where T : class, IMessage<T>, new()
    {
        public ProtoConsumer(string bootstrapServers, string schemaRegistryUrl, string consumerGroup, string topic)
            : base(bootstrapServers, schemaRegistryUrl, consumerGroup, topic)
        {
        }

        public void Build()
        {
            base.AddSchemaRegistry();
            _consumer =
                    new ConsumerBuilder<string, T>(_consumerConfig)
                        .SetValueDeserializer(new ProtobufDeserializer<T>().AsSyncOverAsync())
                        .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                        .Build();

            _consumer.Subscribe(_topic);
        }
    }

}
