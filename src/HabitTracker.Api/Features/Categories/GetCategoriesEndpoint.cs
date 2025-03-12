using D20Tek.Functional.AspNetCore.MinimalApi;
using HabitTracker.Api.Common;
using HabitTracker.Api.Domain;
using HabitTracker.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HabitTracker.Api.Features.Categories;

public static class GetCategoriesEndpoint
{
    public static WebApplication MapGetCategoriesEndpoint(this WebApplication routes)
    {
        routes.MapGet(Constants.Categories.ServiceBase, GetAll)
              .WithTags(nameof(Category))
              .RequireAuthorization()
              .WithOpenApi();

        return routes;
    }

    private static async Task<IResult> GetAll(
        [FromServices] AppDbContext db,
        [FromServices] ILogger<Program> logger,
        ClaimsPrincipal user)
    {
        logger.LogInformation("==> GET CategoriesForUser called");
        var result = await GetCategoriesForUserCommand.Handle(db, user.GetId());
        var msg = result.Match(s => "succeeded", e => e.First().ToString());
        logger.LogInformation($"==> GET CategoriesForUser complete - result: {msg}");
        return result.ToApiResult();
    }
}
