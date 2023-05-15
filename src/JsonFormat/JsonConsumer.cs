using Common;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry.Serdes;

namespace JsonConsole
{
    internal class JsonConsumer<T> : ConsumerBase<T>
        where T : class
    {
        public JsonConsumer(string bootstrapServers, string schemaRegistryUrl, string consumerGroup, string topic)
            : base(bootstrapServers, schemaRegistryUrl, consumerGroup, topic)
        {
        }

        public void Build()
        {
            base.AddSchemaRegistry();

            _consumer =
                    new ConsumerBuilder<string, T>(_consumerConfig)
                        .SetKeyDeserializer(Deserializers.Utf8)
                        .SetValueDeserializer(new JsonDeserializer<T>().AsSyncOverAsync())
                        .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                        .Build();

            _consumer.Subscribe(_topic);
        }
    }
}
