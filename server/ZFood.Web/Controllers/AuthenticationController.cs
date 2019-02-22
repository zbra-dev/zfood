using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using ZFood.Core.API;
using ZFood.Model;
using ZFood.Web.Configuration;
using ZFood.Web.DTO;

namespace ZFood.Web.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService userService;

        private readonly SlackConfiguration slackConfiguration;

        public AuthenticationController(IUserService userService, SlackConfiguration slackConfiguration)
        {
            this.userService = userService;
            this.slackConfiguration = slackConfiguration;
        }

        [HttpGet("slack")]
        public async Task<IActionResult> SlackAuthentication()
        {
            if (!Request.Query.TryGetValue("code", out var codes))
            {
                return BadRequest();
            }

            var code = codes.FirstOrDefault();
            using (var httpClient = new HttpClient())
            {
                var payloadData = new Dictionary<string, string>
                {
                    { "client_id", slackConfiguration.ClientId },
                    { "client_secret", slackConfiguration.ClientSecret },
                    { "code", code },
                };
                var payload = new FormUrlEncodedContent(payloadData);
                var response = await httpClient.PostAsync(slackConfiguration.Url, payload);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new AuthenticationException($"Error authenticating user with {provider}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var slackUser = JsonConvert.DeserializeObject<SlackUserDTO>(responseContent);
                var user = await userService.FindByProviderId(CredentialsProvider.Slack, slackUser.UserInfo.Id);
                if (user == null)
                {
                    var createUserRequest = new CreateUserRequest
                    {
                        Email = slackUser.UserInfo.Email,
                        Name = slackUser.UserInfo.Name,
                        Provider = CredentialsProvider.Slack.ToString(),
                        ProviderId = slackUser.UserInfo.Id,
                        AvatarUrl = slackUser.UserInfo.AvatarUrl,
                    };

                    await userService.CreateUser(createUserRequest);
                }

                return Ok();
            }
        }
    }
}