global using D20Tek.Functional;
global using HabitTracker.Web;
global using HabitTracker.Web.Common;
global using Microsoft.AspNetCore.Components;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args)
                                    .AddBlazorRoot()
                                    .AddPresentationServices();

await builder.Build().RunWithStartup();
