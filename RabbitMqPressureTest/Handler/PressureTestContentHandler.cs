
namespace RabbitMqPressureTest.Handler
{
    using System;
    using Newtonsoft.Json;
    using RabbitMqPressureTest.Model;

    public class PressureTestContentHandler : IRabbitMqPubSubHamdler
    {
        public void Handle(RabbitMqEventStream stream)
        {
            try
            {
                var @event = JsonConvert.DeserializeObject<PressureTestContent>(stream.Data);
                Console.WriteLine($"PressureTestContent: {@event.Content}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PressureTestContentHandler Exception:{ex.Message}");
            }
        }
    }
}
