using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Yumify.Core.Entities;
using Yumify.Core.IRepository;

namespace Yumify.Repository.Repositories
{
    public class CartRepository : ICart
    {
        private readonly IDatabase _database;
        public CartRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<Cart?> GetCart(string id)
        {
            var cart = await _database.StringGetAsync(id);
            var result = cart.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Cart>(cart!);
            return result;
        }
        public async Task<Cart?> CreateUpdateCart(Cart cart)
        {
            var JsonResult = JsonSerializer.Serialize(cart);
            var isSet = await _database.StringSetAsync(cart.Id, JsonResult, TimeSpan.FromDays(2));
            return isSet ? cart : null;
        }

        public async Task<bool> DeleteCart(string id)
        {
            var IsDeleted = await _database.KeyDeleteAsync(id);
            return IsDeleted;
        }

    }
}
