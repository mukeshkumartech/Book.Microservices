using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public interface IDatabaseContext
    {
        SqlConnection GetConnection();
        //Task<SqlConnection> GetConnectionAsync();
        SqlConnection OpenConnection();
        //SqlConnection OpenConnectionAsync();

        void Dispose();
    }
}
