using System;
using System.Collections.Generic;

namespace ProductStoreChallenge.Data
{
    public class Order
    {
        public string Id { get; set; }

        public IEnumerable<OrderItem> Items { get; set; } = new List<OrderItem>();

        public decimal? TotalAmount { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
