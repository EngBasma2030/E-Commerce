using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared;
using Shared.DataTransferObject.ProductModuleDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")] // baseUrl/api/Products
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        // Get All Product
        [HttpGet]
        // GET BaseUrl/api/Products
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParameters queryParameters)
        {
            var Products =await _serviceManager.ProductService.GetAllProductsAsync(queryParameters);
            return Ok(Products);
        }
        // Get Product By Id
        [HttpGet("{id:int}")]
        // GET BaseUrl/api/Products/10
        public async Task<ActionResult<ProductDto>> GetProducts(int id)
        {
            var product =await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }
        // Get All Types
        [HttpGet("types")]
        // GET BaseUrl/api/Products/types
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);
        }
        // Get All Brands
        [HttpGet("brands")]
        // GET BaseUrl/api/Products/brands
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var brands =await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }

    }
}
