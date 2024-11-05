var builder = DistributedApplication.CreateBuilder(args);

/* Add Our IDP */
// TODO: Configure IDP as an aspire resource

/* Our Orleans Cluster & API */
builder.AddProject<Projects.Reach_SiloHost>("reach-silo")
    .WithExternalHttpEndpoints();

/* Add Our Editor */
builder.AddProject<Projects.Reach_EditorApp>("reach-editor");

builder.Build().Run();
