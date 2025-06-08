using Domain.Contracts;
using Domain.Models.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromDays(30));
            if (IsCreatedOrUpdated = true)
                return await GetBasketAsync(basket.Id);
            else
                return null;

        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
            return await _database.KeyDeleteAsync(Key);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string Key)
        {
            var Basket = await _database.StringGetAsync(Key);
            if(Basket.IsNullOrEmpty)
            {
                return null;
            }
            else
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
        }
    }
}
