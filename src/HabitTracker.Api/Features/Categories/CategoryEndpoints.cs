using HabitTracker.Api.Domain;
namespace HabitTracker.Api.Features.Categories;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/v1/category").WithTags(nameof(Category));

        group.MapGet("/user/{userId}", (string userId) =>
        {
            return new [] { new Category() };
        })
        .WithName("GetAllCategories")
        //.RequireAuthorization()
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

        group.MapPost("/", (Category model) =>
        {
            //return TypedResults.Created($"/api/Categories/{model.ID}", model);
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
