using Shop.DataAccess.Abstract;
using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;

namespace Shop.DataAccess
{
    public class CategoryRepository //: ICategoryRepository
    {
        private const string connectionString = "Server=A-305-12;Database=ShopDb;Trusted_Connection=True;";
        private readonly DbProviderFactory providerFactory;

        public CategoryRepository(string provider)
        {
            providerFactory = DbProviderFactories.GetFactory(provider);
        }
        /*
         * 1. Открыть подключение
         * 2. Создать запрос
         * 3. Выполнить запрос
         * 4. Закрыть приложение
         */
        public void Add(Category category)
        {
            using (DbConnection connection = providerFactory.CreateConnection())
            using (DbCommand sqlCommand = connection.CreateCommand())
            {
                //string query = $"Insert Into Categories values(@Id,'{category.CreateDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}','{category.DeletedDate}','{category.Name}', '{category.ImagePath}');";
               // sqlCommand.CommandText = query;

                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@Id";
                parameter.Value = category.Id;
                sqlCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.DateTime;
                parameter.ParameterName = "@CreationDate";
               // parameter.Value = category.CreateDate;
                sqlCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.DateTime;
                parameter.ParameterName = "@DeletedDate";
                parameter.IsNullable = true;
                parameter.Value = category.DeletedDate;
                sqlCommand.Parameters.Add(parameter);

                connection.ConnectionString = connectionString;

                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        sqlCommand.Transaction = transaction;
                        sqlCommand.ExecuteNonQuery();
                        // И так далее тоже самое с другими командами
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        //private void ExecuteCommandsInTransaction(params SqlCommand[] commands)
        //{
        //    using (SqlTransaction transaction = connection.BeginTransaction())
        //    {
        //        try
        //        {
        //            foreach (var command in commands)
        //            {
        //                command.Transaction = transaction;
        //                command.ExecuteNonQuery();
        //                // И так далее тоже самое с другими командами
        //                transaction.Commit();
        //            }
        //        }
        //        catch (SqlException exception)
        //        {
        //            transaction.Rollback();
        //        }
        //    }
        //}

        public void Delete(Guid categoryId)
        {
        }

        public ICollection<Category> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = connection.CreateCommand())
            {
                string query = "Select * From Categories;";
                sqlCommand.CommandText = query;
                
                connection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<Category> categories = new List<Category>();
                while (sqlDataReader.Read())
                {
                    categories.Add(new Category
                    {
                        Id = Guid.Parse(sqlDataReader["id"].ToString()),
                        //CreateDate = DateTime.Parse(sqlDataReader["CreationDate"].ToString()),
                        DeletedDate = DateTime.Parse(sqlDataReader["DeleteDate"].ToString()),
                        Name = sqlDataReader["Name"].ToString(),
                        ImagePath = sqlDataReader["ImagePath"].ToString()

                    });
                }
                return categories;
            }
        }

        public void Update(Category category)
        {

        }
    }
}
