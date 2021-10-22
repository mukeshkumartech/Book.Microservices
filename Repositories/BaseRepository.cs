using Data.Infrastructure;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        protected readonly IUnitOfWork uow;

        /// <summary>
        /// Initialize the connection
        /// </summary>
        /// <param name="uow">UnitOfWork</param>
        public BaseRepository(IUnitOfWork uow)
        {
            if (uow == null) throw new ArgumentNullException("unitOfWork");
            this.uow = uow;
            // _conn = _uow.DataContext.Connection;
        }

        /// <summary>
        /// Creates the IdbCommand
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="connection"></param>
        /// <returns>Returns the instance of IDbCommand</returns>
        public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
        {
            return new SqlCommand
            {
                CommandText = commandText,
                Connection = (SqlConnection)connection,
                CommandType = commandType
            };
        }

        /// <summary>
        /// Creates the sql data adapter
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Returns the instance of sql data adapter</returns>
        public IDataAdapter CreateAdapter(IDbCommand command)
        {
            return new SqlDataAdapter((SqlCommand)command);
        }

        /// <summary>
        /// Creates the sql parameters
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="direction"></param>
        /// <returns>Returns the sql parameter</returns>
        public SqlParameter CreateSqlParameter(string name, object value, DbType dbType,
            ParameterDirection direction)
        {
            return new SqlParameter
            {
                DbType = dbType,
                ParameterName = name,
                Direction = direction,
                Value = value
            };
        }

        /// <summary>
        /// Creates the sql parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="direction"></param>
        /// <returns>Returns the sql parameter</returns>
        public SqlParameter CreateSqlParameter(string name, int size, object value, DbType dbType,
            ParameterDirection direction)
        {
            return new SqlParameter
            {
                DbType = dbType,
                Size = size,
                ParameterName = name,
                Direction = direction != null ? direction : ParameterDirection.Input,
                Value = value
            };
        }

        /// <summary>
        /// Get the data table
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns the datatable</returns>

        public DataTable GetDataTable(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters = null)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }

                var dataset = new DataSet();
                var dataAdaper = this.CreateAdapter(cmd);

                dataAdaper.Fill(dataset);

                return dataset.Tables[0];
            }

        }

        /// <summary>
        /// Get the data set
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns the dataset</returns>

        public DataSet GetDataSet(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters = null)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }

                var dataset = new DataSet();
                var dataAdaper = this.CreateAdapter(cmd);

                dataAdaper.Fill(dataset);

                return dataset;
            }

        }

        /// <summary>
        /// Execute non-query
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns the number</returns>
        public int Execute(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            int i = 0;

            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = commandType;

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    cmd.ExecuteNonQuery();

                    i = (int)((SqlParameter)cmd.Parameters["@id"]).Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        /// <summary>
        /// Execute non-query asynchronously
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns the number</returns>
        public async Task<int> ExecuteAsync(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            int i = 0;

            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = commandType;

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    await cmd.ExecuteNonQueryAsync();

                    i = (int)((SqlParameter)cmd.Parameters["@id"]).Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        /// <summary>
        /// Execute non-query asynchronously
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        public async Task ExecuteAsyncVoid(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = commandType;

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Execute non-query with transaction 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns the sql identity</returns>
        public int ExecuteWithTrans(SqlConnection conn, string commandText, CommandType commandType, SqlTransaction transaction,
           SqlParameter[] parameters)
        {
            int i = 0;

            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = commandType;
                    cmd.Transaction = transaction;

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    i = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        /// <summary>
        /// Execute non-query without transaction
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        public void ExecuteNonQuery(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = commandType;

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Execute non-query with transaction
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <param name="parameters"></param>
        public void ExecuteNonQueryWithTrans(SqlConnection conn,  string commandText, CommandType commandType, 
            SqlTransaction transaction,
           SqlParameter[] parameters)
        {
            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = commandType;
                    cmd.Transaction = transaction;

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the data
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns the instance of sql data reader</returns>
        public SqlDataReader Get(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters)
        {

            try
            {

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = commandType;

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    var reader = cmd.ExecuteReader();

                    return reader;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the data asynchronously
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns the instance of sql data reader</returns>
        public async Task<SqlDataReader> GetAsync(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = commandType;

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    var reader = await cmd.ExecuteReaderAsync();

                    return reader;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Execute scalar asynchronously
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns the number</returns>
        public async Task<int> ExecuteScalarNumberAsync(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = commandType;

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    var result = (int)await cmd.ExecuteScalarAsync();

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Executes scalar asynchronously
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns the string</returns>
        public async Task<string> ExecuteScalarStringAsync(SqlConnection conn, string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = commandType;

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    var result = (string)await cmd.ExecuteScalarAsync();

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
