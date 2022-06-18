using System.Collections.Generic;
using System.Threading.Tasks;
using ConsumerService.Contracts;
using IndividualTask.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsumerService.DAL
{
    public class Repository<T> : IRepository<T> where T : BaseClass // child of interface IRepository
    {
        private readonly NewsContext _context;
        private readonly DbSet<T> _entities;

        public  Repository(NewsContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        //public async Task Insert(T entity)
        //{
        //    await _entities.AddAsync(entity);
        //    await _context.SaveChangesAsync();
        //}
        public void Insert(T entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.SingleOrDefaultAsync(s => s.Id == id);
        }
    }
}
