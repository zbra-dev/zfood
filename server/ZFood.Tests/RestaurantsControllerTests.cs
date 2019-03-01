using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using ZFood.Web.DTO;

namespace ZFood.Tests
{
    public class RestaurantsControllerTests
            : IClassFixture<IntegrationTestsWebApplicationFactory<Web.Startup>>
    {
        private const string Url = "/restaurants";

        private readonly HttpClient client;

        public RestaurantsControllerTests(IntegrationTestsWebApplicationFactory<Web.Startup> factory)
        {
            client = factory.CreateClient();
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(10, 50)]
        [InlineData(50, 100)]
        [InlineData(1000, 1000)]
        [InlineData(1000, 0)]
        public async Task TestGetRestaurants(int skip, int take)
        {
            var url = $"{Url}?skip={skip}&take={take}";
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());

            var page = await response.Content.ReadAsAsync<PageDTO<RestaurantDTO>>();
            Assert.NotNull(page);
            Assert.True(page.Items.Count() <= take);
        }

        [Theory]
        [InlineData("Name Test 1", "Address Test 1")]
        [InlineData("Name Test 2", "Address Test 2")]
        [InlineData("Name Test 3", "Address Test 3")]
        public async Task PostRestaurant(string name, string address)
        {
            var restaurant = new RestaurantDTO
            {
                Name = name,
                Address = address,
            };
            var creationResponse = await client.PostAsJsonAsync(Url, restaurant);
            Assert.Equal(HttpStatusCode.Created, creationResponse.StatusCode);
            Assert.NotNull(creationResponse.Headers.Location);

            var findResponse = await client.GetAsync(creationResponse.Headers.Location);
            Assert.Equal(HttpStatusCode.OK, findResponse.StatusCode);
            Assert.Equal("application/json; charset=utf-8", findResponse.Content.Headers.ContentType.ToString());

            var foundRestaurant = await findResponse.Content.ReadAsAsync<RestaurantDTO>();
            Assert.NotNull(foundRestaurant);
            Assert.Equal(name, foundRestaurant.Name);
            Assert.Equal(address, foundRestaurant.Address);
        }

        [Theory]
        [InlineData("10", "Update Test 1", "Update Test 1")]
        [InlineData("11", "Update Test 2", "Update Test 2")]
        [InlineData("12", "Update Test 3", "Update Test 3")]
        public async Task PutRestaurant(string id, string name, string address)
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
    }
}
