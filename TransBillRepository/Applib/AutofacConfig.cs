
namespace TransBillRepository.Applib
{
    using Autofac;
    using MongoDB.Driver;
    using TransBillRepository.Domain.Model;
    using TransBillRepository.Domain.Repository;

    internal static class AutofacConfig
    {
        private static IContainer container;

        public static IContainer Container
        {
            get
            {
                if (container == null)
                {
                    RegisterContainer();
                }

                return container;
            }
        }

        public static void RegisterContainer()
        {
            var builder = new ContainerBuilder();

            //// SQL
            builder.RegisterType<Persistent.Sql.NewBaseBallARepository>()
                .WithParameter("connectionString", ConfigHelper.ConnectionString)
                .Keyed<IOriginNewBaseBallRepository>(RedisNewBaseBallType.NewBaseBallTypeA)
                .Keyed<IOriginNewBaseBallRepository>(MongoNewBaseBallType.NewBaseBallTypeA)
                .SingleInstance();

            builder.RegisterType<Persistent.Sql.NewBaseBallBRepository>()
                .WithParameter("connectionString", ConfigHelper.ConnectionString)
                .Keyed<IOriginNewBaseBallRepository>(RedisNewBaseBallType.NewBaseBallTypeB)
                .Keyed<IOriginNewBaseBallRepository>(MongoNewBaseBallType.NewBaseBallTypeB)
                .SingleInstance();

            //// Mongo
            builder.RegisterType<Persistent.Mongo.NewBaseBallARepository>()
                .WithParameter("mongoClient", NoSqlService.MongoClient)
                .Keyed<ITargetNewBaseBallRepository>(MongoNewBaseBallType.NewBaseBallTypeA)
                .SingleInstance();

            builder.RegisterType<Persistent.Mongo.NewBaseBallBRepository>()
                .WithParameter("mongoClient", NoSqlService.MongoClient)
                .Keyed<ITargetNewBaseBallRepository>(MongoNewBaseBallType.NewBaseBallTypeB)
                .SingleInstance();

            //// Redis
            builder.RegisterType<Persistent.Redis.NewBaseBallARepository>()
                .WithParameter("conn", NoSqlService.RedisConnections)
                .WithParameter("affixKey", ConfigHelper.RedisAffixKey)
                .WithParameter("dataSet", ConfigHelper.RedisDataSet)
                .Keyed<ITargetNewBaseBallRepository>(RedisNewBaseBallType.NewBaseBallTypeA)
                .SingleInstance();

            builder.RegisterType<Persistent.Redis.NewBaseBallBRepository>()
                 .WithParameter("conn", NoSqlService.RedisConnections)
                .WithParameter("affixKey", ConfigHelper.RedisAffixKey)
                .WithParameter("dataSet", ConfigHelper.RedisDataSet)
                .Keyed<ITargetNewBaseBallRepository>(RedisNewBaseBallType.NewBaseBallTypeB)
                .SingleInstance();

            container = builder.Build();
        }
    }
}
