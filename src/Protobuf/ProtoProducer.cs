using Common;
using Confluent.Kafka;
using Confluent.SchemaRegistry.Serdes;
using Google.Protobuf;

namespace Protobuf
{
    public class ProtoProducer<T> : ProducerBase<T>
        where T : class, IMessage<T>, new()
    {
        public ProtoProducer(string bootstrapServers, string schemaRegistryUrl, string topic)
           : base(bootstrapServers, schemaRegistryUrl, topic)
        {            
        }

        public void Build()
        {
            base.AddSchemaRegistry();

            _producer =
                 new ProducerBuilder<string, T>(_producerConfig)
                    .SetValueSerializer(new ProtobufSerializer<T>(_schemaRegistry))
                    .Build();
        }
    }
}
