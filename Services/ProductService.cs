using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Specification;
using ServicesAbstraction;
using Shared;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork , IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
            return BrandsDto;
        }

        public async Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductQueryParameters parameters)
        {
            var specifications = new ProductWithTypeAndBrandSpecification(parameters);
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var products = await Repo.GetAllAsync(specifications);
            var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            var ProductCount = products.Count();
            var countSpec = new ProductCountSpecifications(parameters);
            var TotalCount = await Repo.CountAsync(countSpec);
            return new PaginationResponse<ProductDto>(parameters.PageIndex, ProductCount, TotalCount, Data);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var specifications = new ProductWithTypeAndBrandSpecification(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specifications);
            return _mapper.Map<Product, ProductDto>(product);
        }
    }
}
