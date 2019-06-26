
namespace TransBillRepository
{
    using System;
    using TransBillRepository.Applib;
    using TransBillRepository.Domain.Model;
    using TransBillRepository.Model;

    class Program
    {
        static void Main(string[] args)
        {
            if (ConfigHelper.TargetRepoType == TargetRepositoryType.None)
            {
                Console.WriteLine("Check Config TargetRepoType Setting");
                Console.Read();
                return;
            }

            var repoType = ConfigHelper.TargetRepoType == TargetRepositoryType.Redis ? typeof(RedisNewBaseBallType) : typeof(MongoNewBaseBallType);
            var newBaseBallTypes = Enum.GetValues(repoType);
            foreach (var type in newBaseBallTypes)
            {

                var compoment = TransNewBaseBallCompoment.GenerateInstance(type);
                compoment.StartProcess();
            }

            Console.Read();
        }
    }
}
