using D20Tek.Functional.AspNetCore.MinimalApi;
using HabitTracker.Api.Common;
using HabitTracker.Api.Domain;
using HabitTracker.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HabitTracker.Api.Features.Categories;

public static class CategoryEndpoints
{
    public static WebApplication MapCategoryEndpoints (this WebApplication routes)
    {
        var group = routes.MapGroup("/api/v1/category")
                          .WithTags(nameof(Category))
                          .RequireAuthorization()
                          .WithOpenApi();

        group.MapGet("/", async ([FromServices] HabitTrackerDbContext db, ClaimsPrincipal user) =>
                (await GetCategoriesForUserCommand.Handle(db, user.GetId())).ToApiResult())
             .WithName("GetAllCategories");

        group.MapGet("/{id}", async ([FromRoute] int id,
                                     [FromServices] HabitTrackerDbContext db,
                                     ClaimsPrincipal user) =>
                (await GetCategoryByIdCommand.Handle(db, new(id, user.GetId()))).ToApiResult())
             .WithName("GetCategoryById");

        group.MapPut("/{id}", async([FromRoute] int id,
                                    [FromBody] UpdateCategoryRequest request,
                                    [FromServices] HabitTrackerDbContext db,
                                    ClaimsPrincipal user) =>
                (await UpdateCategoryCommand.Handle(db, request.AppendUserId(user.GetId()))).ToApiResult())
        .WithName("UpdateCategory");

        group.MapPost("/", async ([FromBody] CreateCategoryRequest request,
                                  [FromServices] HabitTrackerDbContext db,
                                  ClaimsPrincipal user) =>
        {
            var result = await CreateCategoryCommand.Handle(db, request.AppendUserId(user.GetId()));
            var catId = result.Match(c => c.Id, _ => 0);
            return result.ToCreatedApiResult($"/api/categories/{catId}");
        })
        .WithName("CreateCategory");

        group.MapDelete("/{id}", async ([FromRoute] int id,
                                        [FromServices] HabitTrackerDbContext db,
                                        ClaimsPrincipal user) =>
                (await DeleteCategoryCommand.Handle(db, new(id, user.GetId()))).ToApiResult())
            .WithName("DeleteCategory");

        return routes;
    }
}
