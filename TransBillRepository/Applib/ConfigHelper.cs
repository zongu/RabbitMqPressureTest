
namespace TransBillRepository.Applib
{
    using System;
    using System.Configuration;
    using TransBillRepository.Model;

    internal static class ConfigHelper
    {
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

        public static readonly string MongoConnectionString = ConfigurationManager.ConnectionStrings["MongoConnectionString"].ToString();

        public static readonly string RedisConnectionString = ConfigurationManager.ConnectionStrings["RedisConnectionString"].ToString();

        public static readonly string RedisAffixKey = ConfigurationManager.AppSettings["RedisAffixKey"].ToString();

        public static readonly int RedisDataSet = Convert.ToInt32(ConfigurationManager.AppSettings["RedisDataSet"]);

        public static readonly int TimerInterval = Convert.ToInt32(ConfigurationManager.AppSettings["TimerInterval"]);

        public static TargetRepositoryType TargetRepoType
        {
            get
            {
                var typeConf = ConfigurationManager.AppSettings["TargetRepoType"] ?? string.Empty;
                switch (typeConf)
                {
                    case "Redis":
                        return TargetRepositoryType.Redis;
                    case "Mongo":
                        return TargetRepositoryType.Mongo;
                    default:
                        return TargetRepositoryType.None;
                }
            }
        }
    }
}
