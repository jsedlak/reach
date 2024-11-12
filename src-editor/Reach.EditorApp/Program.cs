using Reach.Applets;
using Reach.EditorApp.Components;
using Reach.EditorApp.Runtime;
using Tazor.Components;

var builder = WebApplication.CreateBuilder(args);

// Add our aspire hook
builder.AddServiceDefaults();

// Add our HTTP clients!
// TODO: Move to Service Defaults
builder.Services.AddHttpClient("api", client => client.BaseAddress = new Uri("https://localhost:7208/"));
builder.Services.AddHttpClient("graphql", client => client.BaseAddress = new Uri("https://localhost:7208/"));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Configure authentication
builder.AddAuth0WebApp();

// Add our cascading contexts
builder.AddApplets();

// Add our theming stuff
builder.Services.AddTazorServer().Build();

/* ################################# */
/* BUILD AND EXECUTE THE APPLICATION */
/* ################################# */
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

// Add our auth0 callback endpoints 
app.UseAuth0();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Reach.EditorApp.Client._Imports).Assembly);

app.Run();
