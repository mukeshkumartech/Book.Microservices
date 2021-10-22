using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Infrastructure
{
    public class DatabaseContextFactory : IDatabaseContextFactory
    {
        private IDatabaseContext dataContext;
        private string connString;

        public DatabaseContextFactory(string _connString)
        {
            connString = _connString;
        }

        public IDatabaseContext Context()
        {
            return dataContext ??= new DatabaseContext(this.connString);
        }


        public void Dispose()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
