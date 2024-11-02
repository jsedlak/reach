var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Reach_AppApi>("reach-appapi");

builder.AddProject<Projects.Reach_SiloHost>("reach-silohost");

builder.Build().Run();
