
namespace TransBillRepository.Applib
{
    using System;
    using MongoDB.Driver;
    using StackExchange.Redis;

    internal static class NoSqlService
    {
        private static Lazy<ConnectionMultiplexer> lazyRedisConnections;

        public static ConnectionMultiplexer RedisConnections
        {
            get
            {
                if (lazyRedisConnections == null)
                {
                    NoSqlInit();
                }

                return lazyRedisConnections.Value;
            }
        }

        private static MongoClient mongoClient;

        public static MongoClient MongoClient
        {
            get
            {
                if(mongoClient == null)
                {
                    NoSqlInit();
                }

                return mongoClient;
            }
        }

        private static void NoSqlInit()
        {
            lazyRedisConnections = new Lazy<ConnectionMultiplexer>(() =>
            {
                var options = ConfigurationOptions.Parse(ConfigHelper.RedisConnectionString);
                options.AbortOnConnectFail = false;

                var muxer = ConnectionMultiplexer.Connect(options);
                muxer.ConnectionFailed += (sender, e) =>
                {
                    Console.WriteLine("redis failed: " + EndPointCollection.ToString(e.EndPoint) + "/" + e.ConnectionType);
                };
                muxer.ConnectionRestored += (sender, e) =>
                {
                    Console.WriteLine("redis restored: " + EndPointCollection.ToString(e.EndPoint) + "/" + e.ConnectionType);
                };

                return muxer;
            });

            mongoClient = new MongoClient(ConfigHelper.MongoConnectionString);
        }
    }
}
