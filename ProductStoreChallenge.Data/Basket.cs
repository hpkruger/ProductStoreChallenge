using System;
using System.Collections.Generic;

namespace ProductStoreChallenge.Data
{
    public class Basket
    {
        public IEnumerable<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}
