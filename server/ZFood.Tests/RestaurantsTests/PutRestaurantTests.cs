using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using ZFood.Web.DTO;

namespace ZFood.Tests
{
    public class PutRestaurantTests
        : IClassFixture<IntegrationTestsWebApplicationFactory<Web.Startup>>
    {
        private const string Url = "/restaurants";

        private readonly HttpClient client;

        public PutRestaurantTests(IntegrationTestsWebApplicationFactory<Web.Startup> factory)
        {
            client = factory.CreateClient();
        }

        [Theory]
        [InlineData("10", "Put Test 10", "Put Test 10")]
        [InlineData("20", "Put Test 20", "Put Test 20")]
        [InlineData("30", "Put Test 30", "Put Test 30")]
        [InlineData("40", "Put Test 30", "Put Test 30")]
        public async Task TestPutRestaurant(string id, string name, string address)
        {
            var request = new UpdateRestaurantRequestDTO
            {
                Name = name,
                Address = address,
            };
            var url = $"{Url}/{id}";
            var updateResponse = await client.PutAsJsonAsync(url, request);
            Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("10000", "Put Test 1", "Put Test 1")]
        [InlineData("-1000", "Put Test 2", "Put Test 2")]
        [InlineData("", "Put Test 3", "Put Test 3")]
        [InlineData("", "", "Put Test 3")]
        [InlineData("", "Put Test 3", "")]
        public async Task TestPutNonexistentRestaurant(string id, string name, string address)
        {
            var request = new UpdateRestaurantRequestDTO
            {
                Name = name,
                Address = address,
            };
            var url = $"{Url}/{id}";
            var updateResponse = await client.PutAsJsonAsync(url, request);
            Assert.Equal(HttpStatusCode.NotFound, updateResponse.StatusCode);
        }

        [Theory]
        [InlineData("30", "", "")]
        [InlineData("30", "", "Put Test 30")]
        [InlineData("30", "Put Test 30", "")]
        public async Task TestPutRestaurantWithInvalidRequest(string id, string name, string address)
        {
            var request = new UpdateRestaurantRequestDTO
            {
                Name = name,
                Address = address,
            };
            var url = $"{Url}/{id}";
            var updateResponse = await client.PutAsJsonAsync(url, request);
            Assert.Equal(HttpStatusCode.BadRequest, updateResponse.StatusCode);
        }
    }
}
