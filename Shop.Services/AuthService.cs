using System;
using System.Collections.Generic;
using System.Text;
using Shop.Domain;
using Shop.DataAccess;
using System.Linq;


namespace Shop.Services
{
    public class AuthService
    {
        private Repository repository;

        public AuthService(string connectionString)
        {
            repository = new Repository(connectionString);
        }
        public string SignUp(string email, string password)
        {
            var users = repository.GetAll(new User { Email = email, Password = password });

            if (users.SingleOrDefault(user => user.Email == email).Email == email) return null;
            repository.Add(
                new User
                {
                    Email = email,
                    Password = password
                });

            return "Регистрация прошла успешно";
        }

        public bool SignIn(string email, string password)
        {
            var users = repository.GetAll(new User { Email = email, Password = password });

            if (users.SingleOrDefault(user => user.Email != email).Email != email &&
                users.SingleOrDefault(user => user.Password != password).Password != password)
                return false;
            return true;
        }
    }
}
