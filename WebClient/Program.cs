using WebClient;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SharedClasses.Interfaces;
using WebClient.Services.DALs;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:7113")
    });

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

#region Add Data Access Layers
builder.Services.AddTransient<IBlogPageDAL,ClientBlogPageDAL>();
builder.Services.AddTransient<IPostPageDAL, ClientPostPageDAL>();
#endregion

#region Register Component Libraries
builder.Services.AddBlazorBootstrap();
#endregion

await builder.Build().RunAsync();
