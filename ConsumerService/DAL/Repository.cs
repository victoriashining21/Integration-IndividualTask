using System;
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
           // entity.ToString();
            //if (T entity != bool entity)
            //{
               
            //}
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

        public Task<T> LikesNewsByIdAsync(string id, int like)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> DislikeNewsByIdAsync(string id, int dislike)
        {
            throw new System.NotImplementedException();
        }

        //public Task DeleteAllAsync()
        //{
        //    throw new System.NotImplementedException();
        //}

        //public Task DeleteByTitleAsync(string title)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
