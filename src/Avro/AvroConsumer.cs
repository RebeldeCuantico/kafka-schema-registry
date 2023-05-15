using Common;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry.Serdes;

namespace AvroConsole
{
    public class AvroConsumer<T> : ConsumerBase<T>
        where T : class
    {
        public AvroConsumer(string bootstrapServers, string schemaRegistryUrl, string consumerGroup, string topic)
            : base(bootstrapServers, schemaRegistryUrl, consumerGroup, topic)
        {
        }

        public void Build()
        {
            base.AddSchemaRegistry();
            _consumer =
                    new ConsumerBuilder<string, T>(_consumerConfig)
                        .SetValueDeserializer(new AvroDeserializer<T>(_schemaRegistry).AsSyncOverAsync())
                        .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                        .Build();

            _consumer.Subscribe(_topic);
        }
    }
}
