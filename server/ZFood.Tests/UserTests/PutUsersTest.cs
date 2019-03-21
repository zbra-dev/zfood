using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using ZFood.Web.DTO;

namespace ZFood.Tests.UserTests
{
    public class PutUsersTest
        : IClassFixture<IntegrationTestsWebApplicationFactory<Web.Startup>>
    {
        private const string Url = "/users";

        private readonly HttpClient client;

        public PutUsersTest(IntegrationTestsWebApplicationFactory<Web.Startup> factory)
        {
            client = factory.CreateClient();
        }

        [Theory]
        [InlineData("Test", "test@test", "Slack", "NewProviderId1")]
        public async Task PutUserTest(string name, string email, string provider, string providerId)
        {
            // Creating a user
            var createUserRequest = CreateNewUserRequestDTO(name, email, provider, providerId);
            var creationResponse = await client.PostAsJsonAsync(Url, createUserRequest);

            // Getting this user and their Id from the creation Response
            var newlyCreatedUser = await creationResponse.Content.ReadAsAsync<UserDTO>();
            var uri = creationResponse.Headers.Location.ToString();
            var userId = uri.Substring(23); //TODO: Find a better way to catch the id

            // Updating the user (only name)
            var nameToUpdate = newlyCreatedUser.Name + " Testing1";
            var updateUserRequest = CreateUpdateUserRequestDTO(nameToUpdate, email);
            var url = $"{Url}/{userId}";
            var updateResponse = await client.PutAsJsonAsync(url, updateUserRequest);

            // Getting the updated user and checking consistency
            var urlToGetUser = $"{Url}/{userId}";
            var getUpdatedUserResponse = await client.GetAsync(urlToGetUser);
            var updatedUser = await getUpdatedUserResponse.Content.ReadAsAsync<UserDTO>();

            Assert.Equal(updatedUser.Name, nameToUpdate);
            Assert.Equal(updatedUser.Email, email);

            //TODO: Implement tests for other cases
        }

        private CreateUserRequestDTO CreateNewUserRequestDTO(string name, string email, string provider, string providerId)
        {
            return new CreateUserRequestDTO
            {
                Name = name,
                Email = email,
                Provider = provider,
                ProviderId = providerId
            };
        }

        private UpdateUserRequestDTO CreateUpdateUserRequestDTO(string name, string email)
        {
            return new UpdateUserRequestDTO
            {
                Name = name,
                Email = email
            };
        }
    }
}
