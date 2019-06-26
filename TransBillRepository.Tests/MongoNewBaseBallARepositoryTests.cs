
namespace TransBillRepository.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MongoDB.Driver;
    using TransBillRepository.Domain.Model;
    using TransBillRepository.Persistent.Mongo;

    [TestClass]
    public class MongoNewBaseBallARepositoryTests
    {
        private NewBaseBallARepository repo;

        [TestInitialize]
        public void Init()
        {
            var mongoClient = new MongoClient(Applib.ConfigHelper.MongoConnectionString);
            var db = mongoClient.GetDatabase("Ball");
            db.DropCollection("NewBaseBallA");

            this.repo = new NewBaseBallARepository(mongoClient);
        }

        [TestMethod]
        public void BatchInsertTest()
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
        public void QueryByIncludeAllianceTest()
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
