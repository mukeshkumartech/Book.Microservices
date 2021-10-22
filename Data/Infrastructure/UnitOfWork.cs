using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDatabaseContextFactory _factory;
        private IDatabaseContext _context;
        public IDbTransaction Transaction { get; private set; }

        public UnitOfWork(IDatabaseContextFactory factory)
        {
            _factory = factory;
        }

        public void Commit()
        {
            if (Transaction != null)
            {
                try
                {
                    Transaction.Commit();
                }
                catch (Exception)
                {
                    Transaction.Rollback();
                }

                Transaction.Dispose();
                Transaction = null;
            }
            else
            {
                throw new NullReferenceException("Tryed commit not opened transaction");
            }
        }

        public IDatabaseContext DataContext
        {
            get { return _context ??= _factory.Context(); }
        }

        public IDbTransaction BeginTransaction()
        {
            if (Transaction != null)
            {
                throw new NullReferenceException("Not finished previous transaction");
            }

            Transaction = _context.GetConnection().BeginTransaction();

            return Transaction;
        }

        public async Task<IDbTransaction> BeginTransactionAsync()
        {
            if (Transaction != null)
            {
                throw new NullReferenceException("Not finished previous transaction");
            }

            var conn = _context.GetConnection();
            await conn.OpenAsync();

            Transaction = conn.BeginTransaction();

            return Transaction;
        }


        public void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
            }

            if (_context != null)
            {
                _context.Dispose();
            }
        }


    }
}
