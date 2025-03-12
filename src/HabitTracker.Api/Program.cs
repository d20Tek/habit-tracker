global using D20Tek.Functional;
global using D20Tek.Functional.AspNetCore.MinimalApi;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using System.Security.Claims;

global using HabitTracker.Api;
global using HabitTracker.Api.Common;
global using HabitTracker.Api.Domain;
global using HabitTracker.Api.Features;
global using HabitTracker.Api.Persistence;

var builder = WebApplication.CreateBuilder(args)
                            .AddDatabase()
                            .AddServices()
                            .AddAuth();

builder.Build()
       .ConfigurePipeline()
       .MapEndpoints()
       .Run();
