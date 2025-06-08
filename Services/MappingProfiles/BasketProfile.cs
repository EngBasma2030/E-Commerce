using AutoMapper;
using Domain.Models.BasketModule;
using Shared.DataTransferObject.BasketModulesDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItems, BasketItemDto>().ReverseMap();
        }
    }
}
