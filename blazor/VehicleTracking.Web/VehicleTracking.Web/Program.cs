using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VehicleTracking.Web;
using VehicleTracking.Web.Domain;
using VehicleTracking.Web.Domain.DataServices;
using VehicleTracking.Web.Domain.HttpClients;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
static void RegisterServices(WebAssemblyHostBuilder builder)
{
    var appSettings = new AppSettings();
    builder.Configuration.Bind(appSettings);
    builder.Services.AddSingleton(appSettings);
    builder.Services.AddScoped<VehicleDataService>();
    RegisterHttpClients(builder, appSettings);

}

static void RegisterHttpClients(WebAssemblyHostBuilder builder, AppSettings appSettings)
{
    builder.Services.AddHttpClient<VehicleTrackingHttpClient>(client =>
    {
        client.BaseAddress = new Uri(appSettings.VehicleApiUrl);
        client.Timeout = TimeSpan.FromSeconds(150);
    });
}