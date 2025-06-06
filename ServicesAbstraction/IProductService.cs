using Shared;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IProductService
    {
        // Get All Products
        Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductQueryParameters queryParameters);
        // Get ProductBy Id
        Task<ProductDto> GetProductByIdAsync(int id);
        // Get All Brands
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        // Get All Types 
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();

    }
}
