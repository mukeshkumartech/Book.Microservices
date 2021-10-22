using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        DataTable GetDataTable(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters = null);

        DataSet GetDataSet(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters = null);

        int Execute(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters);
        Task<int> ExecuteAsync(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters);

        Task ExecuteAsyncVoid(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters);
        int ExecuteWithTrans(SqlConnection conn, string commandText, CommandType commandType, SqlTransaction transaction,
            SqlParameter[] parameters);

        void ExecuteNonQuery(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters);

        void ExecuteNonQueryWithTrans(SqlConnection conn, string commandText, CommandType commandType, SqlTransaction transaction,
            SqlParameter[] parameters);
        SqlDataReader Get(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters);

        Task<SqlDataReader> GetAsync(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters);
        Task<int> ExecuteScalarNumberAsync(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters);
        Task<string> ExecuteScalarStringAsync(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters);

    }
}
