
namespace TransBillRepository.Persistent.Sql
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using TransBillRepository.Domain.Model;
    using TransBillRepository.Domain.Repository;

    public class NewBaseBallARepository : IOriginNewBaseBallRepository
    {
        private string connectionString;

        public NewBaseBallARepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Tuple<Exception, IEnumerable<NewBaseBall>> GetAll()
        {
            try
            {
                using (var cn = new SqlConnection(this.connectionString))
                {
                    string sqlStr = @"select * from t_newbaseball_a";
                    var result = cn.Query<NewBaseBall>(sqlStr);

                    return Tuple.Create<Exception, IEnumerable<NewBaseBall>>(null, result);
                }
            }
            catch (Exception ex)
            {
                return Tuple.Create<Exception, IEnumerable<NewBaseBall>>(ex, null);
            }
        }
    }
}
