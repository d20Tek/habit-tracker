using HabitTracker.Api.Domain;
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

        group.MapGet("/", (ClaimsPrincipal user) =>
        {
            var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine($"GET Category => User-Id: {id}");
            return new [] { new Category { CategoryId = 1, Name = "Get Healthy", UserId=$"{id}" } };
        })
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

        group.MapPost("/", ([FromBody]Category model, ClaimsPrincipal user) =>
        {
            model.CategoryId = 2;
            var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine($"POST Category => User-Id: {id}");
            model.UserId = id!;
            return TypedResults.Created($"/api/categories/{model.CategoryId}", model);
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
