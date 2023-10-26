using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Decenea.WebAppAdmin;
using Decenea.WebAppShared;
using Microsoft.JSInterop;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddState(builder.Configuration);
builder.Services.AddWebAppShared(builder.Configuration);
var scope = builder.Services.BuildServiceProvider().CreateAsyncScope();
var service = scope.ServiceProvider.GetService<IJSRuntime>();

await builder.Build().RunAsync();