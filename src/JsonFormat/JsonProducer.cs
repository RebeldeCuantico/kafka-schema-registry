using Common;
using Confluent.Kafka;
using Confluent.SchemaRegistry.Serdes;

namespace JsonConsole
{
    public class JsonProducer<T> : ProducerBase<T>
        where T : class
    {
        private readonly JsonSerializerConfig _jsonSerializerConfig;       

        public JsonProducer(string bootstrapServers, string schemaRegistryUrl, string topic)
            : base(bootstrapServers, schemaRegistryUrl, topic)
        {
            _jsonSerializerConfig = new JsonSerializerConfig
            {
                BufferBytes = 100
            };
        }

        public void Build()
        {
            base.AddSchemaRegistry();

            _producer =
                new ProducerBuilder<string, T>(_producerConfig)
                    .SetValueSerializer(new JsonSerializer<T>(_schemaRegistry, _jsonSerializerConfig))
                    .Build();
        }      

    }
}
