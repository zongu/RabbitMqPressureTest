

namespace RabbitMqPressureTest.Model
{
    using System.ComponentModel;

    public enum RmqMode
    {
        [Description("生產者")]
        Producer,
        [Description("消費者")]
        Consumer
    }
}
