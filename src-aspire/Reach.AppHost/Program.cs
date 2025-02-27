var builder = DistributedApplication.CreateBuilder(args);

// Add our AI configuration
var openAiApiKey = builder.AddParameter("OpenAiApiKey", true);
var openAiModel = builder.AddParameter("OpenAiModel", "gpt-4o-mini");
var ollamaEndpoint = builder.AddParameter("OllamaEndpoint", "http://localhost:11434");
var ollamaModel = builder.AddParameter("OllamaModel", "llama3.3");

/* Add Our IDP */
// TODO: Configure IDP as an aspire resource

///* Adds Streaming Capabilities */
//var eventHubs = builder.AddAzureEventHubs("reach-event-hubs")
//    .RunAsEmulator()
//    .AddEventHub("ReachEvents");

//var eventHubConnectionString = eventHubs.PublishAsConnectionString();

/* PostgreSQL */
var postgres = builder.AddPostgres("postgres");
    // .WithPgWeb();

var membershipDb = postgres.AddDatabase(
    name: "membership-database", 
    databaseName: "membership"
);

/* Add Our Mongo Storage */
var mongo = builder.AddMongoDB("reach-mongo");

/* Our Orleans Cluster & API */
var storage = builder.AddAzureStorage("reach-cluster-storage")
    .RunAsEmulator(r => r.WithImage("azure-storage/azurite", "3.33.0"));

var grainStorage = storage.AddBlobs("grain-state");
var streamingStorage = storage.AddTables("streaming");
var cluster = storage.AddTables("clustering");
var tenantStorage = storage.AddTables("membership-storage");

var orleans = builder.AddOrleans("reach-cluster")
    .WithClusterId("reach")
    .WithServiceId("reach")
    .WithClustering(cluster)
    .WithGrainStorage("Default", grainStorage)
    .WithGrainStorage("PubSubStore", grainStorage)
    .WithMemoryStreaming("StreamProvider");
    //.WithStreaming("StreamProvider", eventHubs);

var silo = builder.AddProject<Projects.Reach_Silo_Host>("reach-silo")
    .WithEnvironment("AI_CHAT", "OpenAI")
    .WithEnvironment("Ollama__Endpoint", ollamaEndpoint)
    .WithEnvironment("Ollama__Model", ollamaModel)
    .WithEnvironment("OpenAi__ApiKey", openAiApiKey)
    .WithEnvironment("OpenAi__Model", openAiModel)
    .WithReference(orleans)
    .WithReference(mongo)
    .WithReference(membershipDb)
    .WaitFor(grainStorage)
    .WaitFor(cluster)
    .WaitFor(streamingStorage)
    .WaitFor(mongo)
    .WaitFor(postgres);

/* Add Our Editor Application */
//builder.AddProject<Projects.Reach_EditorApp>("reach-editor")
//    .WithReference(silo)
//    .WithReference(tenantStorage)
//    .WithReference(membershipDb)
//    .WaitFor(mongo)
//    .WaitFor(silo)
//    .WaitFor(postgres);

builder.AddProject<Projects.Reach_Platform>("reach-platform");

builder.Build().Run();
