using Domain.Models;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    internal class ProductWithTypeAndBrandSpecification : BaseSpecification<Product, int>
    {
        // use this constructor to create query to get produt by id
        public ProductWithTypeAndBrandSpecification(int id) 
            : base(Product => Product.Id == id)
        {
            // AddIncludes
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.productType);
        }

        public ProductWithTypeAndBrandSpecification(ProductQueryParameters parameters)
            : base(ApplyCriteria(parameters)
            )
        {
            // AddIncludes
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.productType);
            ApplySorting(parameters);
            ApplyPagination(parameters.PageSize, parameters.PageIndex);
        }

        private static Expression<Func<Product, bool>> ApplyCriteria(ProductQueryParameters parameters)
        {
            return product =>
                        (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId.Value) &&
                        (!parameters.TypeId.HasValue || product.BrandId == parameters.TypeId.Value) &&
                        (string.IsNullOrEmpty(parameters.Search) || product.Name.ToLower().Contains(parameters.Search.ToLower()));
        }

        private void ApplySorting(ProductQueryParameters parameters)
        {
            switch (parameters.Options)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }
        }
    }
}
