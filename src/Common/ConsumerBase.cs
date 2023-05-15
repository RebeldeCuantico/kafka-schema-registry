using Confluent.Kafka;
using Confluent.SchemaRegistry;

namespace Common
{
    public abstract class ConsumerBase<T>
        where T : class
    {
        protected readonly string _topic;
        protected readonly ConsumerConfig _consumerConfig;
        protected readonly SchemaRegistryConfig _schemaRegistryConfig;
        private CachedSchemaRegistryClient _schemaRegistry;
        protected IConsumer<string, T> _consumer;

        protected ConsumerBase() { }

        public ConsumerBase(string bootstrapServers, string schemaRegistryUrl, string consumerGroup, string topic)
        {
            _topic = topic;

            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = consumerGroup
            };

            _schemaRegistryConfig = new SchemaRegistryConfig
            {
                Url = schemaRegistryUrl
            };
        }

        protected void AddSchemaRegistry()
        {
            _schemaRegistry = new CachedSchemaRegistryClient(_schemaRegistryConfig);
        }

        public T Consume(CancellationToken token)
        {
            try
            {
                var cr = _consumer.Consume(token);
                return cr.Message.Value;
            }
            catch (OperationCanceledException)
            {
                _consumer.Close();
            }

            return default(T);
        }

        public  void Close()
        {
            _consumer.Close();
            _schemaRegistry.Dispose();
        }
    }
}
