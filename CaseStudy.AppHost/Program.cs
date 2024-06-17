using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("RedisConnection");

builder.AddProject<Projects.CaseStudy_Presentation>("presentation")
    .WithReference(redis);

builder.Build().Run();





