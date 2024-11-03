var builder = DistributedApplication.CreateBuilder(args);

/* Add Our IDP */



/* Our Core Application */
//builder.AddProject<Projects.Reach_SiloHost>("reach-silo")
//    .WithExternalHttpEndpoints()
//    .WithReference(keycloak);


builder.AddProject<Projects.Reach_EditorApp>("reach-editor")
    .WithExternalHttpEndpoints();

builder.Build().Run();
