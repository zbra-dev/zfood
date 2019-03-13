using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Xunit;
using ZFood.Web.DTO;

namespace ZFood.Tests.UserTests
{
    public class GetUsersTests
        : IClassFixture<IntegrationTestsWebApplicationFactory<Web.Startup>>
    {
        private const string Url = "/users";

        private readonly HttpClient client;

        public GetUsersTests(IntegrationTestsWebApplicationFactory<Web.Startup> factory)
        {
            client = factory.CreateClient();
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(10, 30)]
        [InlineData(50, 100)]
        [InlineData(250, 250)]
        [InlineData(1000,0)]
        public async Task SearchUsersTest(int skip, int take)
        {
            var url = $"{Url}?skip={skip}&take={take}";
            var response = await client.GetAsync(url);
            var page = await response.Content.ReadAsAsync<PageDTO<UserDTO>>();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.NotNull(page);
            Assert.True(page.Items.Count() <= take);
        }

        [Theory]
        [InlineData(-1, -5)]
        [InlineData(-5, -100)]
        public async Task GetPageOfUsersFailingTest(int skip, int take)
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
        public async Task GetUserByIdTest(string id)
        {
            var url = $"{Url}/{id}";
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("-5")]
        [InlineData("-10")]
        [InlineData("test")]
        [InlineData("@#test#@")]
        public async Task GetNonexistentUserByIdTest(string id)
        {
            var url = HttpUtility.UrlEncode($"{Url}/{id}");
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
