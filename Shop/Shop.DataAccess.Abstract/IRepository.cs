using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.DataAccess.Abstract
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T element);
        void Update(T element);
        ICollection<T> GetAll(T element);
        void Delete(Guid Id);
    }
}
