using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.Tests
{
    public class ShoppingCartTests
    {
        [Fact]
        public async Task AddProduct_ReturnsOk_WhenProductIdAndUserIdIsProvided()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Put, "api/ShoppingCart/AddProduct?productId=88a7f65f-a2a8-401c-a804-3fd7bde7f24f");
            request.Headers.Add("userId", "f91a6abd-2f61-4549-95f0-0527c69189f4");

            var result = await client.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task AddProduct_ReturnsInternalServerError_WhenProductIdIsProvidedAndUserIdIsEmpty()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Put, "api/ShoppingCart/AddProduct?productId=88a7f65f-a2a8-401c-a804-3fd7bde7f24f");

            var result = await client.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, result.StatusCode);
        }

        // Other business cases to be implemented
    }
}