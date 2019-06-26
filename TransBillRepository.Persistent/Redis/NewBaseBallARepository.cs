
namespace TransBillRepository.Persistent.Redis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StackExchange.Redis;
    using TransBillRepository.Domain.Model;
    using TransBillRepository.Domain.Repository;

    public class NewBaseBallARepository : ITargetNewBaseBallRepository
    {
        private int dataSet;

        private string affixKey;

        private ConnectionMultiplexer conn;

        public NewBaseBallARepository(ConnectionMultiplexer conn, string affixKey, int dataSet)
        {
            this.conn = conn;
            this.affixKey = $"{affixKey}:NewBaseBallA";
            this.dataSet = dataSet;
        }

        public bool DropAndBatchInsert(IEnumerable<NewBaseBall> newBaseBalls)
        {
            return UseConnection(redis =>
            {
                redis.KeyDelete(this.affixKey);
                newBaseBalls.ToList().ForEach(n =>
                {
                    redis.ListRightPush(this.affixKey, n.ToString());
                });

                return true;
            });
        }

        public IEnumerable<NewBaseBall> QueryByIncludeAllianceField(IEnumerable<string> alliances)
        {
            return UseConnection(redis =>
            {
                var entryLists = redis.ListRange(this.affixKey);
                var result = entryLists
                .Select(e => NewBaseBall.FromString(e.ToString()))
                .Where(n => alliances.Any(a => n.f_alliance.Contains(a)));

                return result;
            });
        }

        private T UseConnection<T>(Func<IDatabase, T> func)
        {
            var redis = conn.GetDatabase(this.dataSet);
            return func(redis);
        }
    }
}
