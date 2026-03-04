using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Assets.Ui;
using Assets.ApiClient.Generated;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUrl = builder.Configuration["Api:BaseUrl"];

if (string.IsNullOrWhiteSpace(apiBaseUrl))
    throw new InvalidOperationException("Falta Api:BaseUrl en wwwroot/appsettings.json");

builder.Services.AddMudServices();

builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl.TrimEnd('/') + "/")
});

builder.Services.AddScoped<IAssetsApiClient, AssetsApiClient>();

await builder.Build().RunAsync();