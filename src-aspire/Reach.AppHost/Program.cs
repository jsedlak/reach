var builder = DistributedApplication.CreateBuilder(args);

/* Add Our IDP */
// TODO: Configure IDP as an aspire resource

/* Our Orleans Cluster & API */
var storage = builder.AddAzureStorage("reach-cluster-storage").RunAsEmulator();
var grainStorage = storage.AddBlobs("grain-state");
var cluster = storage.AddTables("clustering");

var orleans = builder.AddOrleans("reach-cluster")
    .WithClusterId("reach")
    .WithServiceId("reach")
    .WithClustering(cluster)
    .WithGrainStorage("Default", grainStorage);

builder.AddProject<Projects.Reach_SiloHost>("reach-silo")
    .WithExternalHttpEndpoints()
    .WithReference(orleans)
    .WaitFor(grainStorage)
    .WaitFor(cluster);

/* Add Our Editor Application */
builder.AddProject<Projects.Reach_EditorApp>("reach-editor");

builder.Build().Run();
