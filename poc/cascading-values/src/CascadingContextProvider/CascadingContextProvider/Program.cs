using CascadingContextProvider;
using CascadingContextProvider.Components;
using Microsoft.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// add our cascading values
builder.Services.AddCascadingValue(sp => new TenantContext(
    sp.GetRequiredService<NavigationManager>()
));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

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


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(CascadingContextProvider.Client._Imports).Assembly);

app.Run();
