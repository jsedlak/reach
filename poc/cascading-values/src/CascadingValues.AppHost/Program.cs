var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CascadingContextProvider>("cascadingcontextprovider");

builder.AddProject<Projects.PersistingContextProvider>("persistingcontextprovider");

builder.Build().Run();
