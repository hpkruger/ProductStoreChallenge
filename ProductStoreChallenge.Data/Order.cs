using System;
using System.Collections.Generic;

namespace ProductStoreChallenge.Data
{
    public class Order
    {
        public string Id { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
