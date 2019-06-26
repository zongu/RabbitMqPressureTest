
namespace TransBillRepository.Persistent.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using Dapper;
    using TransBillRepository.Domain.Model;
    using TransBillRepository.Domain.Repository;

    public class NewBaseBallBRepository : IOriginNewBaseBallRepository
    {
        private string connectionString;

        public NewBaseBallBRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Tuple<Exception, IEnumerable<NewBaseBall>> GetAll()
        {
            try
            {
                using (var cn = new SqlConnection(this.connectionString))
                {
                    string sqlStr = @"select * from t_newbaseball_b";
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
