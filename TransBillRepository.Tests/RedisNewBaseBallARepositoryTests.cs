
namespace TransBillRepository.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StackExchange.Redis;
    using TransBillRepository.Domain.Model;
    using TransBillRepository.Persistent.Redis;

    [TestClass]
    public class RedisNewBaseBallARepositoryTests
    {
        private NewBaseBallARepository repo;

        [TestInitialize]
        public void Init()
        {
            var lazyRedisConnections = new Lazy<ConnectionMultiplexer>(() =>
            {
                var options = ConfigurationOptions.Parse(Applib.ConfigHelper.RedisConnectionString);
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

            this.repo = new NewBaseBallARepository(lazyRedisConnections.Value, Applib.ConfigHelper.RedisAffixKey, Applib.ConfigHelper.RedisDataSet);
        }

        [TestMethod]
        public void DropAndBatchInsertTest()
        {
            var nowDateTime = DateTime.Now;
            var newBaseBalls = Enumerable.Range(0, 100).Select(index => new NewBaseBallA()
            {
                id = index,
                f_date = nowDateTime.AddMinutes(index)
            });

            var insertResult = this.repo.DropAndBatchInsert(newBaseBalls);
            Assert.IsTrue(insertResult);
        }

        [TestMethod]
        public void QueryByIncludeAllianceFieldTest()
        {
            var nowDateTime = DateTime.Now;
            var newBaseBalls = Enumerable.Range(0, 100).Select(index => new NewBaseBallA()
            {
                id = index,
                f_alliance = $"{index % 4}Test",
                f_date = nowDateTime.AddMinutes(index)
            });

            var insertResult = this.repo.DropAndBatchInsert(newBaseBalls);
            Assert.IsTrue(insertResult);

            var queryResult = this.repo.QueryByIncludeAllianceField(new List<string>() { "1", "2" });

            Assert.IsNotNull(queryResult);
            Assert.AreEqual(queryResult.Count(), 50);
        }
    }
}
