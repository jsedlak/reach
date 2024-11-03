var builder = DistributedApplication.CreateBuilder(args);

/* Add Our IDP */
var keycloak = builder.AddKeycloakContainer("reach-keycloak");
var realm = keycloak.AddRealm("reach");

/* Our Core Application */
//builder.AddProject<Projects.Reach_SiloHost>("reach-silo")
//    .WithExternalHttpEndpoints()
//    .WithReference(keycloak);


builder.AddProject<Projects.Reach_EditorApp>("reach-editor")
    .WithExternalHttpEndpoints()
    .WithReference(keycloak)
    .WithReference(realm);

builder.Build().Run();
