
namespace TransBillRepository.Domain.Repository
{
    using System.Collections.Generic;
    using TransBillRepository.Domain.Model;

    public interface ITargetNewBaseBallRepository
    {
        bool DropAndBatchInsert(IEnumerable<NewBaseBall> newBaseBalls);

        IEnumerable<NewBaseBall> QueryByIncludeAllianceField(IEnumerable<string> alliances);
    }
}
