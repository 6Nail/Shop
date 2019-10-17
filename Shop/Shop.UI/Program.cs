using Microsoft.Extensions.Configuration;
using Shop.DataAccess;
using Shop.DataAccess.Abstract;
using Shop.Domain;
using Shop.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Data.Common;
using System.Data.SqlClient;

namespace Shop.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appSettings.json", false, true);
            IConfigurationRoot configurationRoot = builder.Build();
            var providerName = configurationRoot.GetSection("AppConfig").GetChildren().Single(item => item.Key == "ProviderName").Value;
            var connectionString = configurationRoot.GetConnectionString("MyConnectionString");

            DbProviderFactories.RegisterFactory(providerName, SqlClientFactory.Instance);

            List<Category> categories = new List<Category>()
            {
                new Category()
                {
                Name = "Phone",
                ImagePath = "C:/",
                },
                new Category()
                {
                Name = "Phone",
                ImagePath = "C:/",
                },
            };
            Category category = new Category()
            {
                Id = Guid.Parse("6C042E03-5AA9-4F66-902D-A3C183E89AE7"),
                Name = "Gal",
                ImagePath = "D:/",
            };

            User user = new User()
            {
                Email = "tnik8080@gmail.com",
                Password = "DD123123",
                Address = "Street Pushkin",
                PhoneNumber = "845714115402",
                VerificationCode = "123"
            };
            Repository<Category> repository = new Repository<Category>(connectionString);
            //repository.Add(category);
            //repository.SaveChanges();
            //category.Name = "Galym";
            //repository.SaveChanges();
            //AuthService auth = new AuthService(connectionString);
            //auth.SignUp("test@gmail.com","222");
            //repository.Delete(Guid.Parse("6C042E03-5AA9-4F66-902D-A3C183E89AE7"));
            //repository.SaveChanges();
            repository.GetAll(category);
        }
    }
}
