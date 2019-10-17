using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Shop.DataAccess
{
    public class AutoLevelConnection
    {
        private static DataTable userTable;
        private static DataTable PeopleTable;
        private readonly DbProviderFactory providerFactory;
        private DbConnection connection;

        public AutoLevelConnection(string connectionString, string provider)
        {
            providerFactory = DbProviderFactories.GetFactory(provider);
            connection = providerFactory.CreateConnection();
            connection.ConnectionString = connectionString;
        }

        public  void dataSet()
        {
            var dataSet = new DataSet("Test");
            CreateUserTable();
            CreatePeopleTable();
            dataSet.Tables.AddRange(new DataTable[]
            {
                userTable, PeopleTable
            });
            dataSet.Relations.Add(PeopleTable.Columns["Id"], userTable.Columns["personId"]);
            FillPeople();
            FillUsers();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            var connection = new SqlConnection("ConnectionString?");
            var selectCommand = new SqlCommand("select * from Table", connection);
            dataAdapter.SelectCommand = selectCommand;
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet);
            dataSet.AcceptChanges();
            dataAdapter.Update(dataSet);


        }

        private static void FillUsers()
        {
            userTable.Rows.Add(1, 1);
        }

        private static void FillPeople()
        {
            var IdRow = PeopleTable.NewRow();
            IdRow.ItemArray = new object[] { 1, "Petrovich" };
            PeopleTable.Rows.Add(IdRow);
            PeopleTable.Rows.Add(2, "Vasilich");
        }

        private static void CreatePeopleTable()
        {
            PeopleTable = new DataTable("People");
            PeopleTable.Columns.Add(new DataColumn
            {
                ColumnName = "FullName",
                AllowDBNull = false,
                Unique = true,
                DataType = typeof(string),
            });

        }

        private static DataTable CreateUserTable()
        {
            userTable = new DataTable("Users");
            userTable.Columns.Add(new DataColumn
            {
                ColumnName = "Id",
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                AllowDBNull = false,
                Unique = true,
                DataType = typeof(int),
            });
            userTable.PrimaryKey = new DataColumn[]
            {
                userTable.Columns["Id"]
            };
            userTable.Columns.Add(new DataColumn
            {
                ColumnName = "personId",
                AllowDBNull = false,
                Unique = true,
                DataType = typeof(int),
            });
            userTable.PrimaryKey = new DataColumn[]
          {
                userTable.Columns["personId"]
          };
            return userTable;
        }
    }
}
