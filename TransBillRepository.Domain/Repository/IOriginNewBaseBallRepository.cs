
namespace TransBillRepository.Domain.Repository
{
    using System;
    using System.Collections.Generic;
    using TransBillRepository.Domain.Model;

    public interface IOriginNewBaseBallRepository
    {
        Tuple<Exception, IEnumerable<NewBaseBall>> GetAll();
    }
}
