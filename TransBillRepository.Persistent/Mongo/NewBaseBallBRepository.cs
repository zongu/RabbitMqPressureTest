
namespace TransBillRepository.Persistent.Mongo
{
    using System.Collections.Generic;
    using System.Linq;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Driver;
    using TransBillRepository.Domain.Model;
    using TransBillRepository.Domain.Repository;

    public class NewBaseBallBRepository : ITargetNewBaseBallRepository
    {
        private const string dbName = "Ball";
        private const string collectionName = "NewBaseBallB";

        private MongoClient client { get; set; }
        private IMongoDatabase db { get; set; }
        private IMongoCollection<NewBaseBallB> collection { get; set; }

        static NewBaseBallBRepository()
        {
            BsonClassMap.RegisterClassMap<NewBaseBallB>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                //cm.MapIdMember(c => c.id);
            });
        }

        public NewBaseBallBRepository(MongoClient mongoClient)
        {
            client = mongoClient;
            db = client.GetDatabase(dbName);
            collection = db.GetCollection<NewBaseBallB>(collectionName);
            collection.Indexes.CreateOne(Builders<NewBaseBallB>.IndexKeys.Descending(p => p.f_date));
        }

        public bool DropAndBatchInsert(IEnumerable<NewBaseBall> newBaseBalls)
        {
            this.db.DropCollection(collectionName);
            var insertObj = newBaseBalls.Select(p => NewBaseBallB.FromString(p.ToString()));
            this.collection.InsertMany(insertObj);
            return true;
        }

        public IEnumerable<NewBaseBall> QueryByIncludeAllianceField(IEnumerable<string> alliances)
        {
            var filter = Builders<NewBaseBallB>.Filter.Or(
                alliances.Select(alliance => Builders<NewBaseBallB>.Filter.Regex(p => p.f_alliance, new BsonRegularExpression(alliance))));

            return this.collection.Find(filter).ToList();
        }
    }
}
