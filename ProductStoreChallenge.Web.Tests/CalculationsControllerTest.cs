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
        public async Task TestCalculateCorrectShippingAmountIfLowerThan50()
        {
            var basket = new Basket
            {
                Items = new[] { new BasketItem { Qty = 1, ProductId = "8A7501FA-8AFA-4A4F-9CDB-7E9B3C86BFC0" }}
            };

            var response = await Client.PostAsJsonAsync("/Calculations/Shipping", basket);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var shippingAmount = await response.Content.ReadAsAsync<decimal>();
            Assert.AreEqual(10, shippingAmount);
        }
        [TestMethod]
        public async Task TestCalculateCorrectShippingAmountIfGreaterThan50()
        {
            var basket = new Basket
            {
                Items = new[] { new BasketItem { Qty = 10, ProductId = "8A7501FA-8AFA-4A4F-9CDB-7E9B3C86BFC0" } }
            };

            var response = await Client.PostAsJsonAsync("/Calculations/Shipping", basket);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var shippingAmount = await response.Content.ReadAsAsync<decimal>();
            Assert.AreEqual(20, shippingAmount);
        }
        [TestMethod]
        public async Task TestCalculateCorrectShippingAmountIfEquals50()
        {
            //Hans: @todo
            Assert.IsTrue(false);
        }

        //Hans: @todo: further tests for validating the subTotal and total amount calculations. I hope you are fine if I skip those because of time constraints
    }
}
