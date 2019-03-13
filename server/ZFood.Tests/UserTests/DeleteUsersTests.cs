using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ZFood.Tests.UserTests
{
    public class DeleteUsersTests
        : IClassFixture<IntegrationTestsWebApplicationFactory<Web.Startup>>
    {
        private const string Url = "/users";

        private readonly HttpClient client;

        public DeleteUsersTests(IntegrationTestsWebApplicationFactory<Web.Startup> factory)
        {
            client = factory.CreateClient();
        }

        [Theory]
        [InlineData("1")]
        [InlineData("25")]
        [InlineData("test")]
        [InlineData("200")]
        public async Task DeleteUserTest(string id)
        {
            var url = $"{Url}/{id}";
            var deleteResponse = await client.DeleteAsync(url);
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }
    }
}
