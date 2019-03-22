using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using ZFood.Web.DTO;

namespace ZFood.Tests.RestaurantTests
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
        [InlineData("10", "Update Test 1", "Update Test 1")]
        [InlineData("11", "Update Test 2", "Update Test 2")]
        [InlineData("12", "Update Test 3", "Update Test 3")]
        public async Task TestPutRestaurant(string id, string name, string address)
        {
            var updateRestaurantRequest = new UpdateRestaurantRequestDTO
            {
                Name = name,
                Address = address,
            };
            var url = $"{Url}/{id}";
            var updateResponse = await client.PutAsJsonAsync(url, updateRestaurantRequest);
            Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);
        }
    }
}
