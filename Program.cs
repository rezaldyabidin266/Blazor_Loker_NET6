using BlazorAgentVs2022.Utility;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using BlazorLoker2022;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddDevExpressBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();

var apiEndPoint_Pelamar = BaseApiUrl.Development_Pelamar;

builder.Services.AddAppSettingServiceAsync(apiEndPoint_Pelamar);

//endpoints.MapFallbackToPage("/_Host");

await builder.Build().RunAsync();


