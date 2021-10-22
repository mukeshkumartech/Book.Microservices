
using Data.Infrastructure;
using Entities;
using Entities.ExceptionHandler;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(IUnitOfWork uow) : base(uow) { }

        public async Task<IEnumerable<Book>> Get(Book bookData)
        {
            var conn = uow.DataContext.GetConnection();
            await conn.OpenAsync();

            List<Book> books = new List<Book>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                this.CreateSqlParameter("@Title", bookData.Title, DbType.String, ParameterDirection.Input),
                this.CreateSqlParameter("@Author", bookData.Author, DbType.String, ParameterDirection.Input),
                this.CreateSqlParameter("@ISBN", bookData.ISBN, DbType.Int64, ParameterDirection.Input),
                };

                var reader = await this.GetAsync(conn, "SP_GET_BOOK", CommandType.StoredProcedure, parameters);

                if (reader != null)
                {
                    while (await reader.ReadAsync())
                    {
                        Book book = new Book();
                        book.Id = Convert.ToInt32(reader["Id"]);
                        book.Title = reader["Title"].ToString();
                        book.Author = reader["Author"].ToString();
                        book.PublicationDate = reader["PublicationDate"].ToString();
                        book.ISBN = Convert.ToInt64(reader["ISBN"]);

                        books.Add(book);
                    }

                    reader.Close();
                    reader.Dispose();
                }
            }
            catch (Exception e)
            {
                throw new ApiException(e.Message.ToString());
            }

            uow.DataContext.Dispose();

            return books;
        }

        public async Task<int> Add(Book book)
        {
            var conn = uow.DataContext.OpenConnection();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                this.CreateSqlParameter("@Title", book.Title, DbType.String, ParameterDirection.Input),
                this.CreateSqlParameter("@Author", book.Author, DbType.String, ParameterDirection.Input),
                 this.CreateSqlParameter("@PublicationDate", book.PublicationDate, DbType.String, ParameterDirection.Input),
                this.CreateSqlParameter("@ISBN", book.ISBN, DbType.Int64, ParameterDirection.Input),                
                this.CreateSqlParameter("@Id", 0, DbType.Int32, ParameterDirection.Output)
                };
                                
                var result = await this.ExecuteAsync(conn, "SP_ADD_BOOK", CommandType.StoredProcedure, parameters);

                return result;
            }
            catch (Exception e)
            {
                throw new ApiException(e.Message.ToString());
            }
        }

        public async Task<int> Update(Book book)
        {
            var conn = uow.DataContext.OpenConnection();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                this.CreateSqlParameter("@Title", book.Title, DbType.String, ParameterDirection.Input),
                this.CreateSqlParameter("@Author", book.Author, DbType.String, ParameterDirection.Input),
                this.CreateSqlParameter("@ISBN", book.ISBN, DbType.Int64, ParameterDirection.Input),
                this.CreateSqlParameter("@Id", book.Id, DbType.Int32, ParameterDirection.Input)
                };

                var result = await this.ExecuteAsync(conn, "SP_UPDATE_BOOK", CommandType.StoredProcedure, parameters);

                return result;
            }
            catch (Exception e)
            {
                throw new ApiException(e.Message.ToString());
            }
        }

        public async Task Delete(int id)
        {
            var conn = uow.DataContext.OpenConnection();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                    this.CreateSqlParameter("@ID",id, DbType.Int32, ParameterDirection.Input)
                };

                await this.ExecuteAsync(conn, "SP_DELETE_BOOK", CommandType.StoredProcedure, parameters);

            }
            catch (Exception e)
            {
                throw new ApiException(e.Message.ToString());
            }
        }
    }
}
