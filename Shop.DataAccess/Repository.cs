using Shop.DataAccess.Abstract;
using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data.Common;

namespace Shop.DataAccess
{
    public class Repository : IDisposable
    {
        private readonly DbConnection connection;

        public object GetAll(User user)
        {
            throw new NotImplementedException();
        }

        private readonly DbProviderFactory providerFactory;
        private readonly DbTransaction transaction;
        private string connectionString;

        public UserRepository Users { get; set; }
        public WholeRepository Whole { get; set; }

        public Repository(string providerName, string connectionString)
        {
            providerFactory = DbProviderFactories.GetFactory(providerName);

            connection = providerFactory.CreateConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            transaction = connection.BeginTransaction();

            Users = new UserRepository(connection, transaction);
            Whole = new WholeRepository(connection, transaction);
        }

        public Repository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Dispose()
        {
            transaction.Commit();
            transaction.Dispose();
            connection.Close();
        }
    }
}

