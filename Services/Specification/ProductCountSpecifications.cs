using Domain.Models;
using Shared.DataTransferObject.ProductModuleDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    public class ProductCountSpecifications :BaseSpecification<Product, int>
    {
        public ProductCountSpecifications(ProductQueryParameters parameters):base(ApplyCriteria(parameters))
        {
            
        }

        private static Expression<Func<Product, bool>> ApplyCriteria(ProductQueryParameters parameters)
        {
            return product =>
                        (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId.Value) &&
                        (!parameters.TypeId.HasValue || product.BrandId == parameters.TypeId.Value) &&
                        (string.IsNullOrEmpty(parameters.Search) || product.Name.ToLower().Contains(parameters.Search.ToLower()));
        }
    }
}
