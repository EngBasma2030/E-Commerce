using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity)
        {
           await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()  =>  await _dbContext.Set<TEntity>().ToListAsync();
        

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
           return await _dbContext.Set<TEntity>().FindAsync(id); // added
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity); // deleted
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity); // modified
        }
    }
}
