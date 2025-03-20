using HabitTracker.Api.Features.Categories;
using HabitTracker.Api.Features.Habits;
using HabitTracker.Api.Features.Weather;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

namespace HabitTracker.Api;

internal static class DependencyInjection
{
    private const string _authDomain = "Auth0:Domain";
    private const string _authAudience = "Auth0:Audience";

    public static WebApplicationBuilder AddAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
            {
                c.Authority = $"https://{builder.Configuration[_authDomain]}";
                c.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = builder.Configuration[_authAudience],
                    ValidIssuer = $"https://{builder.Configuration[_authDomain]}",
                };
            });

        builder.Services.AddAuthorization();

        builder.Services.AddCors(options =>
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            }));

        return builder;
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection()
           .UseCors()
           .UseAuthentication()
           .UseAuthorization();

        return app;
    }

    public static WebApplication MapEndpoints(this WebApplication app) =>
        app.MapEndpointFunc(GetCategoriesEndpoint.MapEndpoint)
           .MapEndpointFunc(GetCategoryByIdEndpoint.MapEndpoint)
           .MapEndpointFunc(CreateCategoryEndpoint.MapEndpoint)
           .MapEndpointFunc(UpdateCategoryEndpoint.MapEndpoint)
           .MapEndpointFunc(DeleteCategoryEndpoint.MapEndpoint)

           .MapEndpointFunc(GetHabitsEndpoint.MapEndpoint)
           .MapEndpointFunc(GetHabitByIdEndpoint.MapEndpoint)
           .MapEndpointFunc(CreateHabitEndpoint.MapEndpoint)
           .MapEndpointFunc(UpdateHabitEndpoint.MapEndpoint)
           .MapEndpointFunc(DeleteHabitEndpoint.MapEndpoint)
           .MapEndpointFunc(MarkHabitEndpoint.MapEndpoint)
           .MapEndpointFunc(UnmarkHabitEndpoint.MapEndpoint)

           .MapWeatherEndpoints();
}
