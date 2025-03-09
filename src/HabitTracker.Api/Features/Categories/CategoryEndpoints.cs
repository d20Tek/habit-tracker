using D20Tek.Functional.AspNetCore.MinimalApi;
using HabitTracker.Api.Common;
using HabitTracker.Api.Domain;
using HabitTracker.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HabitTracker.Api.Features.Categories;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/v1/category")
                          .WithTags(nameof(Category))
                          .RequireAuthorization();

        group.MapGet("/", ([FromServices] HabitTrackerDbContext db, ClaimsPrincipal user) =>
                GetCategoriesForUserCommand.Handle(db, user.GetId()).ToApiResult())
             .WithName("GetAllCategories")
             .WithOpenApi();

        group.MapGet("/{id}", (int id) =>
        {
            //return new Category { ID = id };
        })
        .WithName("GetCategoryById")
        .WithOpenApi();

        group.MapPut("/{id}", (int id, Category input) =>
        {
            return TypedResults.NoContent();
        })
        .WithName("UpdateCategory")
        .WithOpenApi();

        group.MapPost("/", ([FromBody] CreateCategoryRequest request,
                            [FromServices] HabitTrackerDbContext db,
                            ClaimsPrincipal user) =>
        {
            request = request with { UserId = user.GetId() };
            Console.WriteLine($"POST Category => User-Id: {request.UserId}");
            var result = CreateCategoryCommand.Handle(db, request);
            var catId = result.Match(c => c.Id, _ => 0);
            return result.ToCreatedApiResult($"/api/categories/{catId}");
        })
        .WithName("CreateCategory")
        .WithOpenApi();

        group.MapDelete("/{id}", (int id) =>
        {
            //return TypedResults.Ok(new Category { ID = id });
        })
        .WithName("DeleteCategory")
        .WithOpenApi();
    }
}
