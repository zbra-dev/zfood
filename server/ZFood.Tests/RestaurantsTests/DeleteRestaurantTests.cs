using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ZFood.Tests
{
    public class DeleteRestaurantTests
        : IClassFixture<IntegrationTestsWebApplicationFactory<Web.Startup>>
    {
        private const string Url = "/restaurants";

        private readonly HttpClient client;

        public DeleteRestaurantTests(IntegrationTestsWebApplicationFactory<Web.Startup> factory)
        {
            client = factory.CreateClient();
        }

        [Theory]
        [InlineData("30")]
        [InlineData("40")]
        [InlineData("1000")]
        public async Task TestDeleteRestaurant(string id)
        {
            var url = $"{Url}/{id}";
            var deleteResponse = await client.DeleteAsync(url);
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }

        [Theory]
        [InlineData("")]
        public async Task TestDeleteRestaurantWithEmptyId(string id)
        {
            var url = $"{Url}/{id}";
            var deleteResponse = await client.DeleteAsync(url);
            Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
        }
    }
}
