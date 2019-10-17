using Microsoft.Extensions.Configuration;
using Shop.Domain;
using System;
using System.IO;
using System.Linq;
using System.Data.Common;
using System.Data.SqlClient;
using Shop.DataAccess;

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




            Category category = new Category()
            {
                Name = "fdgdfhfgh",
                ImagePath = "C:/data",
            };

            using (var context = new ShopContext(connectionString))
            {
                context.Categories.Add(category);
                var result = context.Categories.ToList();
                context.Categories.Remove(category);

                context.Remove(category);
                context.SaveChanges();
            }

        }
    }
}
