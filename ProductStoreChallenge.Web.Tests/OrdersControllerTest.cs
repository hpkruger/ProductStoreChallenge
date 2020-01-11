using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        //[TestMethod]
        //public async Task TestPlaceOrder(Order order)
        //{
             
        //    //var response = await Client.PostAsJsonAsync .PostAsJsonAsync("/Orders/", null);
        //    //Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        //    //var products = await response.Content.ReadAsAsync<IEnumerable<Product>>();

        //    //Assert.IsNotNull(products);
        //}

    }
}
