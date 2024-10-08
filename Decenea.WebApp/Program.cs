using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using Decenea.WebApp;
using Decenea.WebApp.Abstractions;
using Decenea.WebApp.Apis;
using Decenea.WebApp.Database;
using Decenea.WebApp.Extensions;
using Decenea.WebApp.Helpers;
using Decenea.WebApp.Services;
using Decenea.WebApp.State;
using Microsoft.AspNetCore.Components.Authorization;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<AuthStateProvider>();
builder.Services.AddSingleton<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<AuthStateProvider>());
builder.Services.AddSingleton<IAuthStateProvider>(provider => provider.GetRequiredService<AuthStateProvider>());

builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<IndexedDb>();
builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddTransient<IUserService,UserService>();
builder.Services.AddTransient<IAuthService,AuthService>();
builder.Services.AddTransient<IGlobalFunctionService,GlobalFunctionService>();
builder.Services.AddTransient<IGlobalFunctionService,GlobalFunctionService>();
builder.Services.AddTransient<ICookieService,CookieService>();
builder.Services.AddSingleton<TestContainer>();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 5000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

builder.Services.AddRefitClient<IAuthApi>(RefitHelper.GetSettings())
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("http://localhost:5080"));

builder.Services.AddRefitClient<IUserApi>(RefitHelper.GetSettings())
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("http://localhost:5080"))
    .AddTokenHandler();

builder.Services.AddRefitClient<IGroupApi>(RefitHelper.GetSettings())
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("http://localhost:5080"))
    .AddTokenHandler();

builder.Services.AddRefitClient<ITestApi>(RefitHelper.GetSettings())
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("http://localhost:5080"))
    .AddTokenHandler();

await builder.Build().RunAsync();

