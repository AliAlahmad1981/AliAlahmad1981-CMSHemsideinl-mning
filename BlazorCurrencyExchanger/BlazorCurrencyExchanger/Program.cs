using BlazorCurrencyExchanger;
using BlazorCurrencyExchanger.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient("CurrencyExchangerClient", client =>
    client.BaseAddress = new Uri("http://localhost:1337/api/"));
builder.Services.AddHttpClient("CurrencyClient", client =>
    client.BaseAddress = new Uri("https://api.freecurrencyapi.com/v1/"));
builder.Services.AddSingleton<CurrencyService>();
builder.Services.AddScoped<CurrencyExchangerService>();
await builder.Build().RunAsync();
