using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery, ISpecification<TEntity, TKey> specification)
            where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery; // _dbContext.Set<TEntity>()

            if (specification.Criteria != null)
                query = query.Where(specification.Criteria); //_dbContext.Set<TEntity>().Where(p => p.id == 1)

            //foreach (var include in specification.IncludeExpressions)
            //{
            //    query = query.Include(include);
            //}
            //_dbContext.Set<TEntity>().Where(p => p.id == 1).Include(P => P.ProductType).Include(P => P.ProductBrand);

            query = specification.IncludeExpressions
                .Aggregate(query, (currentQuery, include) => currentQuery.Include(include));
            //_dbContext.Set<TEntity>().Where(p => p.id == 1).Include(P => P.ProductType).Include(P => P.ProductBrand);

            if (specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);
            else if (specification.OrderByDescending != null)
                query = query.OrderByDescending(specification.OrderByDescending);

            if (specification.IsPaginated)
                query = query.Skip(specification.Skip).Take(specification.Take);
            return query;
        }
    }
}
