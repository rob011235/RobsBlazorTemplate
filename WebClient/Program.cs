using WebClient;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SharedClasses.Interfaces;
using WebClient.Services.DataServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();



#region Add Data Access Layers
builder.Services.AddTransient<IBlogPageDataService,ClientBlogPageDataService>();
#endregion

#region Register Component Libraries
builder.Services.AddBlazorBootstrap();
#endregion

await builder.Build().RunAsync();
