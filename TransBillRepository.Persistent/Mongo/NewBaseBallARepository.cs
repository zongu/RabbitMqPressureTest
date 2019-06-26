
namespace TransBillRepository.Persistent.Mongo
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Linq;
    using TransBillRepository.Domain.Model;
    using TransBillRepository.Domain.Repository;

    public class NewBaseBallARepository : ITargetNewBaseBallRepository
    {
        private const string dbName = "Ball";
        private const string collectionName = "NewBaseBallA";

        private MongoClient client { get; set; }
        private IMongoDatabase db { get; set; }
        private IMongoCollection<NewBaseBallA> collection { get; set; }

        static NewBaseBallARepository()
        {
            BsonClassMap.RegisterClassMap<NewBaseBallA>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                //cm.MapIdMember(c => c.id);
            });
        }

        public NewBaseBallARepository(MongoClient mongoClient)
        {
            client = mongoClient;
            db = client.GetDatabase(dbName);
            collection = db.GetCollection<NewBaseBallA>(collectionName);
            collection.Indexes.CreateOne(Builders<NewBaseBallA>.IndexKeys.Descending(p => p.f_date));
        }

        public bool DropAndBatchInsert(IEnumerable<NewBaseBall> newBaseBalls)
        {
            this.db.DropCollection(collectionName);
            var insertObj = newBaseBalls.Select(p => NewBaseBallA.FromString(p.ToString()));
            this.collection.InsertMany(insertObj);
            return true;
        }

        public IEnumerable<NewBaseBall> QueryByIncludeAllianceField(IEnumerable<string> alliances)
        {
            var filter = Builders<NewBaseBallA>.Filter.Or(
                alliances.Select(alliance => Builders<NewBaseBallA>.Filter.Regex(p => p.f_alliance, new BsonRegularExpression(alliance))));

            return this.collection.Find(filter).ToList();
        }
    }
}
