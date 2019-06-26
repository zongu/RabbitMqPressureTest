
namespace TransBillRepository.Model
{
    using System.ComponentModel;

    public enum TargetRepositoryType
    {
        [Description("None")]
        None,
        [Description("Redis持久層")]
        Redis,
        [Description("Mongo持久層")]
        Mongo
    }
}
