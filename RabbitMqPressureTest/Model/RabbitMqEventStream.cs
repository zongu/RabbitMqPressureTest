
namespace RabbitMqPressureTest.Model
{
    public class RabbitMqEventStream : EventStream
    {
        public RabbitMqEventStream(string type, string data, long utcTimeStamp)
        {
            Type = type;
            Data = data;
            UtcTimeStamp = utcTimeStamp;
        }
    }

    public interface IRabbitMqPubSubHamdler : IPubSubHandler<RabbitMqEventStream>
    {

    }
}
