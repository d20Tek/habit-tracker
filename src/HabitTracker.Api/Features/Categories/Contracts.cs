using HabitTracker.Api.Domain;

namespace HabitTracker.Api.Features.Categories;

internal record CategoryResponse(int Id, string Name, string UserId)
{
    public static CategoryResponse FromEntity(Category category) =>
        new(category.CategoryId, category.Name, category.UserId);
}

internal record CreateCategoryRequest(string Name, string UserId)
{
    public Category ToEntity() => Category.Create(Name, UserId);
}
