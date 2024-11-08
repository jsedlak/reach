var builder = DistributedApplication.CreateBuilder(args);

/* Add Our IDP */
// TODO: Configure IDP as an aspire resource

/* Adds Streaming Capabilities */
var eventHubs = builder.AddAzureEventHubs("reach-event-hubs")
    .RunAsEmulator()
    .AddEventHub("ReachEvents");

var eventHubConnectionString = eventHubs.PublishAsConnectionString();

/* Add Our Mongo Storage */
var mongo = builder.AddMongoDB("reach-mongo");

/* Our Orleans Cluster & API */
var storage = builder.AddAzureStorage("reach-cluster-storage").RunAsEmulator();
var grainStorage = storage.AddBlobs("grain-state");
var streamingStorage = storage.AddTables("streaming");
var cluster = storage.AddTables("clustering");

var orleans = builder.AddOrleans("reach-cluster")
    .WithClusterId("reach")
    .WithServiceId("reach")
    .WithClustering(cluster)
    .WithGrainStorage("Default", grainStorage)
    .WithGrainStorage("PubSubStore", grainStorage)
    .WithStreaming("StreamProvider", eventHubs);

builder.AddProject<Projects.Reach_Silo_Host>("reach-silo")
    .WithExternalHttpEndpoints()
    .WithReference(orleans)
    .WithReference(mongo)
    .WithReference(eventHubs)
    .WithReference(eventHubConnectionString, "EventHubsConnectionString")
    .WaitFor(grainStorage)
    .WaitFor(cluster)
    .WaitFor(streamingStorage)
    .WaitFor(mongo);

/* Add Our Editor Application */
builder.AddProject<Projects.Reach_EditorApp>("reach-editor");

builder.Build().Run();
