using Microsoft.AspNetCore.Mvc.RazorPages;
using ZFood.Web.Configuration;

namespace ZFood.Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SlackConfiguration slackConfiguration;

        public string ClientId => slackConfiguration?.ClientId;

        public LoginModel(SlackConfiguration slackConfiguration)
        {
            this.slackConfiguration = slackConfiguration;
        }
    }
}