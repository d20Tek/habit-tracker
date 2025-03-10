using D20Tek.Functional;
using HabitTracker.Api.Common;
using HabitTracker.Api.Domain;
using HabitTracker.Api.Persistence;

namespace HabitTracker.Api.Features.Categories;

internal static class CreateCategoryCommand
{
    public static Result<CategoryResponse> Handle(HabitTrackerDbContext db, CreateCategoryRequest request) =>
        TryExcept.Run(
            () => request.Validate()
                         .Map(c => CreateEntity(db, c)),
            ex => Result<CategoryResponse>.Failure(ex));

    private static Result<Category> Validate(this CreateCategoryRequest request) =>
        ValidationErrors.Create()
                        .AddIfError(() => string.IsNullOrEmpty(request.UserId),
                                  Constants.UserIdRequiredError("CreateCategory"))
                        .AddIfError(() => string.IsNullOrEmpty(request.Name),
                                  "CreateCategory.Name",
                                  "Category name is a required.")
                        .AddIfError(() => request.Name.Length > 10,
                                  "CreateCategory.Name",
                                  "Category name must be less than 100 characters.")
                        .Map(() => new Category { Name = request.Name, UserId = request.UserId });

    private static CategoryResponse CreateEntity(HabitTrackerDbContext db, Category c) =>
        db.Categories.Add(c).ToIdentity()
          .Iter(e => db.SaveChanges())
          .Map(r => new CategoryResponse(r.Entity.CategoryId, r.Entity.Name, r.Entity.UserId));
}
