using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;

namespace HabitTracker.Web;

internal static class DependencyInjection
{
    private const string _serverApi = "ServerAPI";
    private const string _serviceUrlConfig = "ServiceUrl";
    private const string _authConfigSection = "Auth0";
    private const string _authResponseType = "code";
    private const string _authAudience = "audience";
    private const string _authAudienceConfig = "Auth0:Audience";
    private const string _serviceTestSleepDelay = "TestSleepDelay";

    public static WebAssemblyHostBuilder AddBlazorRoot(this WebAssemblyHostBuilder builder)
    {
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        return builder;
    }

    public static WebAssemblyHostBuilder AddPresentationServices(this WebAssemblyHostBuilder builder) =>
        builder.AddHttpClient()
               .AddOicdAuth()
               .AddSessionStorage();

    private static WebAssemblyHostBuilder AddHttpClient(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<CustomAuthorizationMessageHandler>();
        builder.Services.AddHttpClient(_serverApi, client => client.BaseAddress = builder.GetServiceUri())
                        .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(_serverApi));

        Constants.ServiceSleepDelay = builder.Configuration.GetValue<int>(_serviceTestSleepDelay, 0);

        return builder;
    }

    private static Uri GetServiceUri(this WebAssemblyHostBuilder builder) =>
        new(builder.Configuration[_serviceUrlConfig] ?? string.Empty);

    private static WebAssemblyHostBuilder AddOicdAuth(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddOidcAuthentication(options =>
        {
            builder.Configuration.Bind(_authConfigSection, options.ProviderOptions);
            options.ProviderOptions.ResponseType = _authResponseType;
            options.ProviderOptions.AdditionalProviderParameters.Add(_authAudience, builder.GetAudienceConfig());
        });

        return builder;
    }

    private static string GetAudienceConfig(this WebAssemblyHostBuilder builder) =>
        builder.Configuration[_authAudienceConfig] ?? string.Empty;

    private static WebAssemblyHostBuilder AddSessionStorage(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddBlazoredSessionStorage(config => {
            config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
            config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
            config.JsonSerializerOptions.WriteIndented = false;
        });

        return builder;
    }
}
