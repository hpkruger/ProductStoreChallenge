using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductStoreChallenge.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProductStoreChallenge.Web.Tests
{
    [TestClass]
    public class OrdersControllerTest
    {
        private static IWebHost TestWebApiHost { get; set; }
        private HttpClient Client { get; set; } = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };

        [ClassInitialize]
        public static void InitTests(TestContext context)
        {
            // Hans: Alternatively, we could use the asp.net TestServer classes here. 
            TestWebApiHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            TestWebApiHost.Start();
        }
        [ClassCleanup]
        public static void Cleanup()
        {
            TestWebApiHost.StopAsync().GetAwaiter().GetResult();
        }
        [TestMethod]
        public async Task TestPlaceOrder()
        {
            var order = new Order
            {
                Items = new[] { new OrderItem { Qty = 1, ProductId = "8A7501FA-8AFA-4A4F-9CDB-7E9B3C86BFC0" }, new OrderItem { Qty = 2, ProductId = "2E09B84B-9980-4096-9FC6-1F6E8E846779" } }
            };

            var response = await Client.PostAsJsonAsync("/Orders/", order);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var returnedOrder = await response.Content.ReadAsAsync<Order>();

            Assert.IsFalse(string.IsNullOrEmpty(returnedOrder.Id));
            Assert.IsNotNull(returnedOrder.CreatedAt);
            Assert.IsNotNull(returnedOrder.Items);
            Assert.IsNotNull(returnedOrder.TotalAmount);
            Assert.IsTrue(returnedOrder.TotalAmount > 0);
        }
    }
}
