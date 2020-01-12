using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStoreChallenge.Data
{
    public class OrderRepository : IOrderRepository
    {
        // Hans: Life time of repositories are request scoped, however, the OrderDictionary is static and will be used in a concurrent fashion, so we should use concurrent collections here. Not catering for concurrently changing a specific order though.
        private static IDictionary<string, Order> OrderDictionary = new ConcurrentDictionary<string, Order>();

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return OrderDictionary.Values;
        }
        public async Task<Order> GetAsync(string id)
        {
            if (OrderDictionary.TryGetValue(id, out Order order))
            {
                return order;
            }
            return null;
        }
        public async Task<Order> CreateOrUpdateAsync(Order order)
        {
            // Hans: Update existing one
            if (!string.IsNullOrEmpty(order.Id) && OrderDictionary.TryGetValue(order.Id, out Order existingOrder)) {
                existingOrder.Items = order.Items;
                existingOrder.UpdatedAt = DateTime.UtcNow;

                return existingOrder;
            }
            // Hans: Insert new order
            order.Id = Guid.NewGuid().ToString();
            order.CreatedAt = DateTime.UtcNow;

            OrderDictionary[order.Id] = order;

            return order;
        }

    }
}
