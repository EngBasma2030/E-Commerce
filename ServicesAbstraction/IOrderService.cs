using Shared.DataTransferObject.OrderDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IOrderService
    {
        // create order
        // Creating Order Will Take Basket Id , Shipping Address , Delivery Method Id , Customer Email
        // And Return Order Details (Id , UserName , OrderDate , Items (Product Name - Picture Url - Price - Quantity)
        // Address , Delivery Method Name , Order Status Value , Sub Total , Total Price  

        Task<OrderToReturnDto> CreateOrder(OrderDto orderDto, string email);

        // Get DeliveryMethods
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync();

        // Get All Orders 
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email);

        // Get Order By Id
        Task<OrderToReturnDto> GetOrderByIdAsync(Guid id);
    }
}
