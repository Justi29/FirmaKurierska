using FirmaKurierska.BlazorClient;
using FirmaKurierska.BlazorClient.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IClientService, ClientService>();


// modyfikacja klienta http aby pobiera³ dane z pliku konfiguracyjnego
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetValue<string>("FirmaKurierskaAPIUrl"))
});


await builder.Build().RunAsync();
