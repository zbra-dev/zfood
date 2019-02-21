# ZFood Server

## User Secrets
The server uses ASP.NET Core's user secrets feature to store sensitive data such as API Keys. More info can be found at https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.2.
Example user secret structure:
```
{
  "Slack": {
    "AppId": "api_id",
    "ClientId": "client_id",
    "ClientSecret": "client_secret",
    "SigningSecret": "signing_secret",
    "Url": "url"
  }
}
```