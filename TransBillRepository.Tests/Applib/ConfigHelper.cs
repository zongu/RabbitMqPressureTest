
namespace TransBillRepository.Tests.Applib
{
    public static class ConfigHelper
    {
        public static readonly string ConnectionString = @"Server=35.187.242.76;database=ts111_ball;uid=Test_BD;pwd=BDHz93Gh5X9k7P2W;";

        public static readonly string MongoConnectionString = @"mongodb://localhost:27017";

        public static readonly string RedisConnectionString = @"localhost:6379";

        public static readonly string RedisAffixKey = @"Ball";

        public static readonly int RedisDataSet = 0;
    }
}
