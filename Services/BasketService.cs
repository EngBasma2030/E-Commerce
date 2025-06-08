using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.BasketModule;
using ServicesAbstraction;
using Shared.DataTransferObject.BasketModulesDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasket(BasketDto basket)
        {
            var CustomerBasket = mapper.Map<BasketDto, CustomerBasket>(basket);
            var createdOrUpdatedBasket = await basketRepository.CreateOrUpdateBasketAsync(CustomerBasket);
            if (createdOrUpdatedBasket != null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Can not Create Or Update Basket");
        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
            return await basketRepository.DeleteBasketAsync(Key);
        }

        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var Basket = await basketRepository.GetBasketAsync(Key);
            if (Basket != null)
                return mapper.Map<CustomerBasket, BasketDto>(Basket);
            else
                throw new BasketNotFoundException(Key);
        }
    }
}
