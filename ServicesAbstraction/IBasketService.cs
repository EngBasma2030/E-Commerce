using Shared.DataTransferObject.BasketModulesDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(string Key);
        Task<BasketDto> CreateOrUpdateBasket(BasketDto basket);
        Task<bool> DeleteBasketAsync(string Key);
    }
}
