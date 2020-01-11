using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStoreChallenge.Data
{
    // Hans: Since we are working with IProductRepository throughout the Challenge, we can inject a different implementation that retrieves the products from a proper datastore, e.g. database, later.
    public class ProductRepository : IProductRepository
    {
        private static readonly IDictionary<string, Product> ProductsDictionary = new []
        {
            new Product { Id = "8A7501FA-8AFA-4A4F-9CDB-7E9B3C86BFC0", Name = "Fisher-Price Smart Stages Piggy ", Price = 11.99},
            new Product { Id = "2E09B84B-9980-4096-9FC6-1F6E8E846779", Name = "Green Toys Airplane", Price = 14.99},
            new Product { Id = "30951A58-9A77-40AE-A814-9B7892AEE0AE", Name = "Johnson's Sleepy Baby Gift Set", Price = 10.99},
            new Product { Id = "B8BB716A-93D1-4838-A7FA-057D84DAC15B", Name = "Green Toys Helicopter", Price = 19.99},
            new Product { Id = "2C8E90E9-5CBE-49C7-BCEF-0255350BE25F", Name = "Green Toys Construction Vehicle", Price = 34.99},
            new Product { Id = "6974384A-FBC9-4E6A-B604-156E004D7509", Name = "The First Years First Rattle", Price = 7.99},
            new Product { Id = "947094F0-35A6-4024-9946-AF7E6B318421", Name = "HOMOFY Baby Toys Funny Hammer", Price = 15.99}
        }.ToDictionary(item => item.Id, item => item);

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            // Hans: I declared all methods in the repository class "async" even though the actual implementation is synchronous. 
            // In a real-world scenario fetching data from a datastore, e.g. database, would be done in a asynchronous fashion allowing the request handling thread to be returned back to the aspnet threadpool while the data is retrieved from datastore
            return ProductsDictionary.Values;
        }
        public async Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<string> ids)
        {
            // Hans: Instead of having dedicated methods in the repository for every conditional data retrieval operation, e.g. GetByIds, GetByName etc, an alternative could be to let GetAllAsync return IQueryable instead of IEnumerable and thus deferring the retrieval of the product data.
            // If for example the underlying Linq provider is the Entity Framework, a conditional Linq query on IQueryable will then be translated to an equivalent SQL query in the database.
            // I prefer a more explicit approach though by having dedicated repository methods for every conditional data retrieval, e.g. GetByIdsAsync
            var products = new List<Product>();
            ids.ToList().ForEach(id =>
            {
                // Hans: Since we are using a dictionary here as a data store, a product can be looked up in O(1).
                if (ProductsDictionary.TryGetValue(id, out Product product)) {
                    products.Add(product);
                }
            });
            return products;
        }
    }
}
