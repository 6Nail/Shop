using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Shop.DataAccess
{
    public class UserRepository : IDisposable
    {
        private readonly DbConnection connction;
        private readonly DbProviderFactory providerFactory;
        private DbConnection connection;

        public UserRepository(DbConnection connection)
        {
            this.connection = connection;
        }

        public UserRepository(DbConnection connection, DbTransaction transaction)
        {
            this.connection = connection;
        }

        public UserRepository(string providerName, string connectionString)
        {
            providerFactory = DbProviderFactories.GetFactory(providerName);

            connction = providerFactory.CreateConnection();
            connction.ConnectionString = connectionString;
            connction.Open();
        }

        public void Dispose()
        {
            connction.Close();
        }
    }
}
