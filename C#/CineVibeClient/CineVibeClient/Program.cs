using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CineVibeClient;
using Blazored.LocalStorage;
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient for C# API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5220/") }); 

// HttpClient for Java API
builder.Services.AddHttpClient("JavaAPI", client => client.BaseAddress = new Uri("http://localhost:8080/"));




builder.Services.AddBlazoredLocalStorage();


await builder.Build().RunAsync();