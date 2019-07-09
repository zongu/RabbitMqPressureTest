
namespace RabbitMqPressureTest.Compoment
{
    using RabbitMqPressureTest.Applibs;
    using RabbitMqPressureTest.Model;

    public class ConsumerCompoment : BasicCompoment
    {
        public ConsumerCompoment()
        {
            RabbitMqFactory.Start(ConfigHelper.RabbitMqUri);

            var consumer = new RabbitMqConsumer(
                ConfigHelper.SubQueueNames,
                new PubSubDispatcher<RabbitMqEventStream>(AutoFacConfig.Container),
                ConfigHelper.QueueId);
            consumer.Register();
        }

        public void ProcessStart()
        {
        }

        public void ProcessStop()
        {
            RabbitMqFactory.Stop();
        }
    }
}
