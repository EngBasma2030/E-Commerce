using AutoMapper;
using Domain.Models;
using Domain.Models.ProductModule;
using Shared.DataTransferObject.ProductModuleDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.productType.Name));

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();

        }
    }

}
