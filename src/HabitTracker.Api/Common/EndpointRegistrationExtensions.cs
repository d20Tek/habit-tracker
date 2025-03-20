namespace HabitTracker.Api.Common;

internal static class EndpointRegistrationExtensions
{
    public static WebApplication MapEndpointFunc(this WebApplication app, Func<WebApplication, WebApplication> func) =>
        func(app);
}
