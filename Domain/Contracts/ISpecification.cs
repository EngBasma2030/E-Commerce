using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISpecification<TEntity,T> where TEntity : BaseEntity<T>
    {
        Expression<Func<TEntity, bool>> Criteria { get; } // p => p.Id == 1
        List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
        Expression<Func<TEntity, object>> OrderBy { get; } // Ascending
        Expression<Func<TEntity, object>> OrderByDescending { get; }

        int Skip { get; }
        int Take { get; } 
        bool IsPaginated { get; }
    }
}
