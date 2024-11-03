using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Reach.EditorApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add our aspire hook
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Configure authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddKeycloakWebApp(builder.Configuration);

var app = builder.Build();

// app.UseAuthentication();
// app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
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
    .AddAdditionalAssemblies(typeof(Reach.EditorApp.Client._Imports).Assembly);




app.Run();
