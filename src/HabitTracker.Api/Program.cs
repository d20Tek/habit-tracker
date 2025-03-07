using HabitTracker.Api;
using HabitTracker.Api.Features;
using HabitTracker.Api.Persistence;

var builder = WebApplication.CreateBuilder(args)
                            .AddDatabase()
                            .AddServices();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        }));

builder.Build()
       .ConfigurePipeline()
       .MapEndpoints()
       .Run();
