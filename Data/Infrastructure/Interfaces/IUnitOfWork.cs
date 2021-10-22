using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Data.Infrastructure
{
    public interface IUnitOfWork
    {
        IDatabaseContext DataContext { get; }

        IDbTransaction BeginTransaction();

        void Commit();
    }
}
