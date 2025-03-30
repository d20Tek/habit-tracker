using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace HabitTracker.Web;

internal static class StartupActions
{
    public static async Task RunWithStartup(this WebAssemblyHost host)
    {
        await host.WarmUpBackendApi();
        await host.RunAsync();
    }

    private static async Task WarmUpBackendApi(this WebAssemblyHost host)
    {
        try
        {
            // start a WebApi query to warm up the backend service that may be asleep in Azure.
            var httpClient = host.Services.GetRequiredService<HttpClient>();
            _ = await httpClient.GetStringAsync(Constants.ServiceHealthUrl);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
