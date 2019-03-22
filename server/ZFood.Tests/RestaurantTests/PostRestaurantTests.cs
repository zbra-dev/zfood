using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using ZFood.Core.API;
using ZFood.Web.DTO;

namespace ZFood.Tests.RestaurantTests
{
    public class PostRestaurantTests
        : IClassFixture<IntegrationTestsWebApplicationFactory<Web.Startup>>
    {
        private const string Url = "/restaurants";

        private readonly HttpClient client;

        public PostRestaurantTests(IntegrationTestsWebApplicationFactory<Web.Startup> factory)
        {
            client = factory.CreateClient();
        }

        [Theory]
        [InlineData("Name Test 1", "Address Test 1")]
        [InlineData("Name Test 2", "Address Test 2")]
        [InlineData("Name Test 3", "Address Test 3")]
        public async Task TestPostRestaurant(string name, string address)
        {
            var createRestaurantRequest = new CreateRestaurantRequestDTO
            {
                Name = name,
                Address = address,
            };
            var creationResponse = await client.PostAsJsonAsync(Url, createRestaurantRequest);
            Assert.Equal(HttpStatusCode.Created, creationResponse.StatusCode);
            Assert.NotNull(creationResponse.Headers.Location);

            var newlyCreatedRestaurantResponse = await client.GetAsync(creationResponse.Headers.Location);
            Assert.Equal(HttpStatusCode.OK, newlyCreatedRestaurantResponse.StatusCode);
            Assert.Equal("application/json; charset=utf-8", newlyCreatedRestaurantResponse.Content.Headers.ContentType.ToString());
            
            var newlyCreatedRestaurant = await newlyCreatedRestaurantResponse.Content.ReadAsAsync<RestaurantDTO>();
            Assert.NotNull(newlyCreatedRestaurant);
            Assert.Equal(name, newlyCreatedRestaurant.Name);
            Assert.Equal(address, newlyCreatedRestaurant.Address);
        }

        [Theory]
        [InlineData("Name Test 4", "")]
        [InlineData("", "Address Test 4")]
        [InlineData("", "")]
        public async Task TestPostRestaurantWithEmptyValues(string name, string address)
        {
            var createRestaurantRequest = new CreateRestaurantRequestDTO
            {
                Name = name,
                Address = address,
            };
            var creationResponse = await client.PostAsJsonAsync(Url, createRestaurantRequest);
            Assert.Equal(HttpStatusCode.BadRequest, creationResponse.StatusCode);
        }

        [Theory]
        [InlineData("Name Test 1", "Address Test 1")]
        [InlineData("Name Test 2", "Address Test 2")]
        [InlineData("Name Test 3", "Address Test 3")]
        public async Task TestPostRestaurantThatAlreadyExists(string name, string address)
        {
            var createRestaurantRequest = new CreateRestaurantRequestDTO
            {
                Name = name,
                Address = address,
            };
            await client.PostAsJsonAsync(Url, createRestaurantRequest);
            // Posting twice intentionally
            var creationResponse = await client.PostAsJsonAsync(Url, createRestaurantRequest);
            Assert.Equal(HttpStatusCode.BadRequest, creationResponse.StatusCode);
        }
    }
}
