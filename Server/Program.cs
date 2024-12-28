using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Server.Components;
using Server.Components.Account;
using Server.Data;
using Server.Services;
using Server.Services.DALs;
using SharedClasses.Interfaces;
using SharedClasses.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient();

#region Configuration Database 
//Verify database folder exists 
//var appDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
//if (!Directory.Exists(appDir))
//{
//    Directory.CreateDirectory(appDir);
//}
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddTransient<ManageBlogsPageDAL>();
builder.Services.AddTransient<ManageBlogPageDAL>();
builder.Services.AddTransient<IBlogPageDAL, BlogPageDAL>();
builder.Services.AddTransient<IPostPageDAL, PostPageDAL>();
builder.Services.AddTransient<NavigationDAL>();
#endregion

#region Confgure Web Api
builder.Services.AddControllers().AddJsonOptions(options =>
{
    //Ignore cycles to avoid self referencing problems. See https://learn.microsoft.com/en-us/ef/core/querying/related-data/serialization
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
#endregion

#region Configure Authentication Authorization
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();
#endregion

#region Configure Identity
builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();
#endregion

#region Configure Email
// Configure email
// Secrets.json needs the password in the following format
// "MailSettings": {
//    "Password": "VQ.sQ246wKFe5:Y"
//  },
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
builder.Services.AddTransient<IEmailService, MailKitEmailService>();
builder.Services.AddTransient<IEmailSender<ApplicationUser>, EmailSender>();
#endregion

#region Configure Component Libraries
builder.Services.AddBlazorBootstrap();
#endregion

var app = builder.Build();

#region Execute Migratons
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}
#endregion

#region Add HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(WebClient._Imports).Assembly);
#endregion

#region Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();
#endregion


#region Add default admin user
//User Secrets needs the admin user id and password 
//In the following format
//  "Admin": {
//              "Email": "{username}",
//              "Password": "{password}"
//  },
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            IdentityRole roleRole = new IdentityRole(role);
            await roleManager.CreateAsync(roleRole);
        }
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    string? email = builder.Configuration.GetSection("Admin:Email").Value;
    string? password = builder.Configuration.GetSection("Admin:Password").Value;
    if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) && await userManager.FindByNameAsync(email) == null)
    {
        var user = new ApplicationUser();
        user.Email = email;
        user.UserName = email;
        user.EmailConfirmed = true;
        var results = await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");
    }
}
#endregion

#region Add Api Endpoints
app.MapControllers();
#endregion

app.Run();
