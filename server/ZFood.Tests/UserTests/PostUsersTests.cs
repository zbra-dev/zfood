using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using ZFood.Web.DTO;

namespace ZFood.Tests.UserTests
{
    public class PostUsersTests
        : IClassFixture<IntegrationTestsWebApplicationFactory<Web.Startup>>
    {
        private const string Url = "/users";

        private readonly HttpClient client;

        public PostUsersTests(IntegrationTestsWebApplicationFactory<Web.Startup> factory)
        {
            client = factory.CreateClient();
        }

        [Theory]
        [InlineData("Test", "test@test1.com", "Slack", "NewIdTest1")]
        [InlineData("Test2", "test@test2.com", "Slack", "NewIdTest2")]
        public async Task PostNewUserTest(string name, string email, string provider, string providerId)
        {
            var createUserRequest = BuildCreateUserRequestDTO(name, email, provider, providerId);

            var creationResponse = await client.PostAsJsonAsync(Url, createUserRequest);
            Assert.Equal(HttpStatusCode.Created, creationResponse.StatusCode);
            Assert.NotNull(creationResponse.Headers.Location);
            
            // Getting the newly created User
            var newlyCreatedUserResponse = await client.GetAsync(creationResponse.Headers.Location);
            Assert.Equal(HttpStatusCode.OK, newlyCreatedUserResponse.StatusCode);
            Assert.Equal("application/json; charset=utf-8", newlyCreatedUserResponse.Content.Headers.ContentType.ToString());

            // Ensuring the new user was created correctly
            var newlyCreatedUser = await newlyCreatedUserResponse.Content.ReadAsAsync<UserDTO>();
            Assert.NotNull(newlyCreatedUser);
            Assert.Equal(name, newlyCreatedUser.Name);
            Assert.Equal(email, newlyCreatedUser.Email);
        }

        private CreateUserRequestDTO BuildCreateUserRequestDTO(string name, string email, string provider, string providerId)
        {
            return new CreateUserRequestDTO
            {
                Name = name,
                Email = email,
                Provider = provider,
                ProviderId = providerId
            };
        }

        [Theory]
        [InlineData("Test", "test@testing1.com", "Slack", "NewIdTest1")]
        [InlineData("Test2", "test@test2.com", "Slack", "NewIdTest2")]
        public async Task PostAlreadyExistingUserTest(string name, string email, string provider, string providerId)
        {
            var createUserRequest = BuildCreateUserRequestDTO(name, email, provider, providerId);
            await client.PostAsJsonAsync(Url, createUserRequest);
            var creationResponse = await client.PostAsJsonAsync(Url, createUserRequest); 
            Assert.Equal(HttpStatusCode.BadRequest, creationResponse.StatusCode);
            // TODO: Check specific cases (email / providerId for an existing User)
        }

        [Theory]
        [InlineData("Test", "test@testing1.com", "Test", "NewIdTest1")]
        [InlineData("Test", "test@testing2.com", "Test2", "NewIdTest2")]
        public async Task PostUserWithAnInvalidProvider(string name, string email, string provider, string providerId)
        {
            var createUserRequest = BuildCreateUserRequestDTO(name, email, provider, providerId);
            var creationResponse = await client.PostAsJsonAsync(Url, createUserRequest);
            Assert.Equal(HttpStatusCode.BadRequest, creationResponse.StatusCode);
        }
    }
}
