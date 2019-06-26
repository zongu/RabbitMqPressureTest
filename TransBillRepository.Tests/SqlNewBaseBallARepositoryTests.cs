
namespace TransBillRepository.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TransBillRepository.Persistent.Sql;

    [TestClass]
    public class SqlNewBaseBallARepositoryTests
    {
        private NewBaseBallARepository repo;

        [TestInitialize]
        public void Init()
        {
            this.repo = new NewBaseBallARepository(Applib.ConfigHelper.ConnectionString);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var getAllResult = this.repo.GetAll();
            Assert.IsNull(getAllResult.Item1);
            Assert.IsNotNull(getAllResult.Item2);
        }
    }
}
