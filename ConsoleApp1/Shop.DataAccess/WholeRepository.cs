using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Shop.DataAccess
{
    public class WholeRepository
    {
        private readonly DbConnection connection;
        private readonly DbProviderFactory providerFactory;
        private DbTransaction transaction;

        public UserRepository User { get; set; }
        public WholeRepository(string providerName, string connectionString)
        {
            providerFactory = DbProviderFactories.GetFactory(providerName);
            
            connection = providerFactory.CreateConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            User = new UserRepository(connection);
        }

        public WholeRepository(DbConnection connection, DbTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}
