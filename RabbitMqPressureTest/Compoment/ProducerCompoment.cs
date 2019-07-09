
namespace RabbitMqPressureTest.Compoment
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Timers;
    using RabbitMqPressureTest.Applibs;
    using RabbitMqPressureTest.Model;

    public class ProducerCompoment : BasicCompoment
    {
        private Timer tr;

        public ProducerCompoment()
        {
            RabbitMqFactory.Start(ConfigHelper.RabbitMqUri);
            RabbitMqFactory.GetChannel(ConfigHelper.QueueId);
            this.tr = new Timer();
            this.tr.Interval = ConfigHelper.ProducerInterval * 1000;
            this.tr.AutoReset = true;
            this.tr.Elapsed += TimerElapsed;
        }

        public void ProcessStart()
        {
            this.tr.Start();
        }

        public void ProcessStop()
        {
            RabbitMqFactory.Stop();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Parallel.ForEach(Enumerable.Range(0, ConfigHelper.ProducerBatchCount), (index) =>
            {
                RabbitMqProducer.Publish(
                    ConfigHelper.QueueId, 
                    new PressureTestContent()
                    {
                        Content = $"{DateTime.Now.ToString("yyyyMMddhhmmssfff")}-{index}",
                        CreateDateTime = DateTime.Now
                    });
            });
        }
    }
}
