
namespace RabbitMqPressureTest.Applibs
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    internal static class ConfigHelper
    {
        public static readonly string RabbitUserName = ConfigurationManager.AppSettings["RabbitUserName"].ToString();

        public static readonly string RabbitPassword = ConfigurationManager.AppSettings["RabbitPassword"].ToString();

        public static readonly string RabbitMqUri = ConfigurationManager.AppSettings["RabbitMqUri"].ToString();

        public static readonly IEnumerable<string> SubQueueNames = ConfigurationManager.AppSettings["SubQueueNames"].ToString().Split(',');

        public static readonly string QueueId = ConfigurationManager.AppSettings["QueueId"].ToString();

        public static readonly string RmqExpiration = ConfigurationManager.AppSettings["RmqExpiration"].ToString();

        public static readonly int ProducerInterval = Convert.ToInt32(ConfigurationManager.AppSettings["ProducerInterval"]);

        public static readonly int ProducerBatchCount = Convert.ToInt32(ConfigurationManager.AppSettings["ProducerBatchCount"]);
    }
}
