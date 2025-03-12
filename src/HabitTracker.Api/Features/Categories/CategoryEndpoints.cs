namespace HabitTracker.Api.Features.Categories;

public static class CategoryEndpoints
{
    public static WebApplication MapCategoryEndpoints (this WebApplication routes)
    {
        var group = routes.MapGroup(Constants.Categories.ServiceBase)
                          .WithTags(nameof(Category))
                          .RequireAuthorization()
                          .WithOpenApi();

        //group.MapGet("/{id}", async ([FromRoute] int id,
        //                             [FromServices] AppDbContext db,
        //                             ClaimsPrincipal user) =>
        //        (await GetCategoryByIdCommand.Handle(db, new(id, user.GetId()))).ToApiResult())
        //     .WithName(Constants.Categories.GetByIdName);

        group.MapPut("/{id}", async([FromRoute] int id,
                                    [FromBody] UpdateCategoryRequest request,
                                    [FromServices] AppDbContext db,
                                    ClaimsPrincipal user) =>
                (await UpdateCategoryCommand.Handle(db, request.AppendUserId(user.GetId()))).ToApiResult())
        .WithName(Constants.Categories.UpdateName);

        group.MapPost("/", async ([FromBody] CreateCategoryRequest request,
                                  [FromServices] AppDbContext db,
                                  ClaimsPrincipal user) =>
        {
            var result = await CreateCategoryCommand.Handle(db, request.AppendUserId(user.GetId()));
            var catId = result.Match(c => c.Id, _ => 0);
            return result.ToCreatedApiResult($"{Constants.Categories.ServiceBase}/{catId}");
        })
        .WithName(Constants.Categories.CreateName);

        //group.MapDelete("/{id}", async ([FromRoute] int id,
        //                                [FromServices] AppDbContext db,
        //                                ClaimsPrincipal user) =>
        //        (await DeleteCategoryCommand.Handle(db, new(id, user.GetId()))).ToApiResult())
        //    .WithName(Constants.Categories.DeleteName);

        return routes;
    }
}
