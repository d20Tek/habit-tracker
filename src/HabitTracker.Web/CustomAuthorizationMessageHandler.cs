using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace HabitTracker.Web;

public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public CustomAuthorizationMessageHandler(IAccessTokenProvider provider, NavigationManager navigation)
        : base(provider, navigation)
    {
        // Set the specific API URL
        ConfigureHandler(
            authorizedUrls: ["https://localhost:7050"],
            scopes: ["https://habit-track-api"]
        );
    }
}