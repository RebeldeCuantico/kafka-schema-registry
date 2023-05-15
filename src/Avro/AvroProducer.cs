using Common;
using Confluent.Kafka;
using Confluent.SchemaRegistry.Serdes;

namespace AvroConsole
{
    public class AvroProducer<T> : ProducerBase<T>
        where T : class
    {
        public AvroProducer(string bootstrapServers, string schemaRegistryUrl, string topic)
           : base(bootstrapServers, schemaRegistryUrl, topic)
        {
        }

        public void Build()
        {
            base.AddSchemaRegistry();

            _producer =
                 new ProducerBuilder<string, T>(_producerConfig)
                    .SetValueSerializer(new AvroSerializer<T>(_schemaRegistry))
                    .Build();
        }
    }
}
