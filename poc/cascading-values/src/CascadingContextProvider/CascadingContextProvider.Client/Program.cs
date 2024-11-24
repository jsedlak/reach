using CascadingContextProvider;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// add our cascading values
builder.Services.AddCascadingValue(sp => new TenantContext(
    sp.GetRequiredService<NavigationManager>()
));

await builder.Build().RunAsync();
