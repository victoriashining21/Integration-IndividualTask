using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndividualTask.Models;

namespace ConsumerService.Contracts
{
    public interface IRepository<T> where T : BaseClass
    {
        void Insert(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
    }
}
