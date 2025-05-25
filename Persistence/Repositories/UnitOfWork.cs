using Domain.Contracts;
using Domain.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            // Get Type Name => product 
            var typeName = typeof(TEntity).Name;
            // Dic<string , object> => string Key [name of entity] , object => object from generic Repository of TEntity

            if (_repositories.ContainsKey(typeName))
                return (IGenericRepository<TEntity,TKey>) _repositories[typeName];

            else
            {
                // create object 
                var repo = new GenericRepository<TEntity, TKey>(_dbContext);
                // store object in dic
                _repositories[typeName] = repo;
                // return object
                return repo;
            }
        }

        public async Task<int> SavaChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
