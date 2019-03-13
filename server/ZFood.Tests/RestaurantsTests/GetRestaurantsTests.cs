using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Xunit;
using ZFood.Web.DTO;

namespace ZFood.Tests
{
    public class GetRestaurantsTests
        : IClassFixture<IntegrationTestsWebApplicationFactory<Web.Startup>>
    {
        private const string Url = "/restaurants";

        private readonly HttpClient client;

        public GetRestaurantsTests(IntegrationTestsWebApplicationFactory<Web.Startup> factory)
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
        [InlineData(0, -20)]
        [InlineData(-10, 0)]
        [InlineData(-10, -20)]
        [InlineData(-100, -200)]
        [InlineData(-1000, -2000)]
        public async Task TestGetRestaurantsWithInvalidSetOfValues(int skip, int take)
        {
            var url = $"{Url}?skip={skip}&take={take}";
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("5")]
        [InlineData("10")]
        [InlineData("15")]
        [InlineData("20")]
        [InlineData("25")]
        public async Task TestGetRestaurant(string id)
        {
            var url = $"{Url}/{id}";
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("-10")]
        [InlineData("-1000")]
        [InlineData("test")]
        [InlineData("#test@")]
        [InlineData("!test*")]
        public async Task TestGetRestaurantWithInvalidId(string id)
        {
            var url = HttpUtility.UrlEncode($"{Url}/{id}");
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
