using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

namespace HabitTracker.Api;

internal static partial class DependencyInjection
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
}
