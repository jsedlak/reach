using Reach.EditorApp.Components;
using Reach.EditorApp.Runtime;
using Tazor.Components;
using Microsoft.AspNetCore.Authorization;
using Reach.EditorApp.Security;
using Reach.Apps.ContentApp.Components.Pages;
using Microsoft.AspNetCore.Components;
using Reach.Components.Context;

var builder = WebApplication.CreateBuilder(args);

/* #################################### */
/* BUILD THE APPLICATION                */
/* #################################### */

// Add our aspire hook
builder.AddServiceDefaults();
builder.Services.AddHttpForwarderWithServiceDiscovery();

// grab storage provider
builder.AddKeyedAzureTableClient("membership-storage");

// Add our HTTP clients!
// TODO: Move to Service Defaults
builder.Services.AddHttpClient("global", client => client.BaseAddress = new Uri("https://reach-silo/")).AddServiceDiscovery();
builder.Services.AddHttpClient("api", client => client.BaseAddress = new Uri("https://reach-silo/")).AddServiceDiscovery();
builder.Services.AddHttpClient("graphql", client => client.BaseAddress = new Uri("https://reach-silo/")).AddServiceDiscovery();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization(options => options.SerializeAllClaims = true);

builder.Services.AddControllers();

// Configure authentication
// builder.AddAuth0WebApp();

// Configure authorization
builder.Services.AddAuthorization();
builder.Services.AddSingleton<IAuthorizationHandler, TenantAuthorizationHandler>();

// Add support for our tenant identification and selection
builder.Services.AddCascadingValue(static sp => 
    new CascadingValueSource<TenantContext>(new TenantContext(), false)
);

// Configure Membership & Tenancy
// builder.AddAzureTablesMembership();
// builder.AddPostgresMembership("membership-database");

// Add our cascading contexts
// builder.AddApplets(
//     typeof(ContentEditorPage).Assembly
// );

// Add our theming stuff
builder.Services.AddTazorServer().Build();

Console.WriteLine(builder.Configuration.GetDebugView());

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

// configure out postgres membership
// app.UsePostgresMembership();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Reach.EditorApp.Client._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(Reach.Apps.ContentApp.Components._Imports).Assembly);

app.MapControllers();

// TODO: Formalize into an extension (MapClusterProxy)
app.MapForwarder("/api", "https://reach-silo/api").RequireAuthorization();
app.MapForwarder("/graphql", "https://reach-silo/graphql", opt => opt.CopyRequestHeaders = true)
    .RequireAuthorization();

app.Run();
