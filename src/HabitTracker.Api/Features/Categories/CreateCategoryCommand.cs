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

    private static Result<Category> Validate(this CreateCategoryRequest request)
    {
        var errors = new List<Error>();

        if (string.IsNullOrEmpty(request.UserId))
            errors.Add(Constants.UserIdRequiredError("CreateCategory"));
        if (string.IsNullOrEmpty(request.UserId))
            errors.Add(Error.Validation("CreateCategory.Name", "Category name is a required."));

        return errors.Count > 0 ?
            Result<Category>.Failure([.. errors]) :
            new Category { Name = request.Name, UserId = request.UserId };
    }

    private static CategoryResponse CreateEntity(HabitTrackerDbContext db, Category c) =>
        db.Categories.Add(c).ToIdentity()
          .Iter(e => db.SaveChanges())
          .Map(r => new CategoryResponse(r.Entity.CategoryId, r.Entity.Name, r.Entity.UserId));
    //{
    //    var result = db.Categories.Add(c);
    //    db.SaveChanges();
    //    return new CategoryResponse(result.Entity.CategoryId, result.Entity.Name, result.Entity.UserId);
    //}
}
