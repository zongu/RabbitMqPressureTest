
namespace RabbitMqPressureTest.Applibs
{
    using System;
    using System.Text;
    using Newtonsoft.Json;
    using RabbitMQ.Client;
    using RabbitMqPressureTest.Model;

    internal static class RabbitMqProducer
    {
        public static void Publish<T>(string topicName, T data)
        {
            var channel = RabbitMqFactory.GetChannel(topicName);
            var es = new RabbitMqEventStream(
                typeof(T).Name,
                JsonConvert.SerializeObject(data),
                TimeStampHelper.ToUtcTimeStamp(DateTime.Now));

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(es));
            var prop = channel.CreateBasicProperties();
            prop.Expiration = ConfigHelper.RmqExpiration;

            channel.BasicPublish(
                $"Exchange-{ExchangeType.Direct}-{topicName}",
                string.Empty,
                prop,
                body);
        }
    }
}
