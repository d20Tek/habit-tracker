using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace HabitTracker.Web;

public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
{
    private const string _serviceUrlConfig = "ServiceUrl";
    private const string _authAudienceConfig = "Auth0:Audience";

    public CustomAuthorizationMessageHandler(
        IAccessTokenProvider provider,
        NavigationManager navigation,
        IConfiguration config)
        : base(provider, navigation)
    {
        // Set the specific API URL
        var serviceUrl = config[_serviceUrlConfig] ?? string.Empty;
        var apiScope = config[_authAudienceConfig] ?? string.Empty;
        ConfigureHandler(authorizedUrls: [serviceUrl], scopes: [apiScope]);
    }
}