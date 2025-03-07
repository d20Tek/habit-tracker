using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HabitTracker.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args)
                                    .AddBlazorRoot()
                                    .AddPresentationServices();

await builder.Build().RunAsync();
