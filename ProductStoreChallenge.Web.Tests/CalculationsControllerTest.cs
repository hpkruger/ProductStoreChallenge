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
    public class CalculationsControllerTest
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
        public async Task TestCalculateShippingCosts()
        {
            var basketToCalculate = new Basket
            {
                Items = new[] { new BasketItem { Qty = 2, ProductId = "8A7501FA-8AFA-4A4F-9CDB-7E9B3C86BFC0" }, new BasketItem { Qty = 5, ProductId = "B8BB716A-93D1-4838-A7FA-057D84DAC15B" } }
            };

            var response = await Client.GetAsync("/Calculations/ShippingCosts");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var products = await response.Content.ReadAsAsync<IEnumerable<Product>>();

            Assert.IsNotNull(products);
        }
    }
}
