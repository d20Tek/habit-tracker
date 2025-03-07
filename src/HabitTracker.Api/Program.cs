using HabitTracker.Api;
using HabitTracker.Api.Features;
using HabitTracker.Api.Persistence;

var builder = WebApplication.CreateBuilder(args)
                            .AddDatabase()
                            .AddServices()
                            .AddAuth();

builder.Build()
       .ConfigurePipeline()
       .MapEndpoints()
       .Run();
