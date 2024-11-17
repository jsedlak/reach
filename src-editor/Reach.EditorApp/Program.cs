using Reach.EditorApp.Components;
using Reach.EditorApp.Runtime;
using Tazor.Components;
using Microsoft.AspNetCore.Authorization;
using Reach.EditorApp.Security;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

/* #################################### */
/* BUILD THE APPLICATION                */
/* #################################### */

// Add our aspire hook
builder.AddServiceDefaults();

// Add our HTTP clients!
// TODO: Move to Service Defaults
builder.Services.AddHttpClient("api", client => client.BaseAddress = new Uri("https://localhost:7208/"));
builder.Services.AddHttpClient("graphql", client => client.BaseAddress = new Uri("https://localhost:7208/"));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization(options => options.SerializeAllClaims = true);

// Configure authentication
builder.AddAuth0WebApp();

builder.Services.AddAuthorization();
builder.Services.AddSingleton<IAuthorizationHandler, TenantAuthorizationHandler>();

// Add our cascading contexts
builder.AddApplets(
    typeof(Reach.Apps.ContentApp.Components.ContentEditor).Assembly
);

// Add our theming stuff
builder.Services.AddTazorServer().Build();

/* #################################### */
/* EXECUTE THE APPLICATION              */
/* #################################### */
var app = builder.Build();

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

// Add our auth0 callback endpoints 
app.UseAuth0();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Reach.EditorApp.Client._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(Reach.Apps.ContentApp.Components._Imports).Assembly);

app.Run();
