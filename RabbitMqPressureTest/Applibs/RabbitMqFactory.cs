
namespace RabbitMqPressureTest.Applibs
{
    using System;
    using System.Collections.Concurrent;
    using RabbitMQ.Client;

    internal static class RabbitMqFactory
    {
        private static ConnectionFactory factory;

        private static IConnection connection;

        private static ConcurrentDictionary<string, IModel> models = new ConcurrentDictionary<string, IModel>();

        private static bool TryAddModel(string topicName)
        {
            if (!models.ContainsKey(topicName))
            {
                var chennel = connection.CreateModel();
                chennel.ExchangeDeclare($"Exchange-{ExchangeType.Direct}-{topicName}", ExchangeType.Direct);
                models.TryAdd(topicName, chennel);

                return true;
            }

            return false;
        }

        public static IModel GetChannel(string topicName)
        {
            TryAddModel(topicName);
            return models[topicName];
        }

        public static void Start()
        {
            factory = new ConnectionFactory()
            {
                UserName = ConfigHelper.RabbitUserName,
                Password = ConfigHelper.RabbitPassword,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(5)
            };

            connection = factory.CreateConnection(AmqpTcpEndpoint.ParseMultiple(ConfigHelper.RabbitMqUri));
        }

        public static void Stop()
        {
            foreach (var model in models)
            {
                model.Value.Abort();
                model.Value.Close();
            }

            models = new ConcurrentDictionary<string, IModel>();
            connection.Abort();
            connection.Close();
            factory = null;
        }
    }
}
